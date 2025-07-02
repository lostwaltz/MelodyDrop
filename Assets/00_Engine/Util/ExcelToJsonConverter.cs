using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using ExcelDataReader;
using Newtonsoft.Json;

// script by https://bonnate.tistory.com/355

namespace Engine
{
    public static class ExcelToJsonConverter
    {
        public static string ConvertExcelToJson(string filePath)
        {
            // 엑셀 파일을 읽습니다.
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader;

            switch (Path.GetExtension(filePath))
            {
                case ".xls":
                    // Excel 97-2003 포맷 
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    break;
                case ".xlsx":
                    // Excel 2007 포맷 
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    break;
                default:
                    Debug.LogError("지원하지 않는 포맷입니다. .xls과 .xlsx 파일만 지원합니다.");
                    return null;
            }

            // 엑셀 파일에서 데이터 읽기
            DataSet dataSet = reader.AsDataSet();
            reader.Close();

            // 첫 번째 시트
            DataTable table = dataSet.Tables[0];

            // 1행: 컬럼명, 2행: 타입정보, 3행부터 데이터
            if (table.Rows.Count < 3)
            {
                Debug.LogError("엑셀에 데이터가 충분하지 않습니다. 최소 3행 이상 필요합니다.");
                return null;
            }

            // 타입정보 읽기 (2행)
            List<string> columnTypes = new List<string>();
            for (int j = 0; j < table.Columns.Count; j++)
            {
                string typeStr = table.Rows[1][j]?.ToString().Trim();
                columnTypes.Add(typeStr);
            }

            List<Dictionary<string, object>> jsonData = new List<Dictionary<string, object>>();

            for (int i = 2; i < table.Rows.Count; i++) // 3행부터 데이터 시작
            {
                DataRow row = table.Rows[i];
                Dictionary<string, object> rowData = new Dictionary<string, object>();

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string key = table.Rows[0][j].ToString(); // 컬럼명 (1행)
                    string type = columnTypes[j]; // 타입정보 (2행)
                    object value = row[j];

                    if (string.IsNullOrEmpty(type))
                    {
                        // 타입정보 없으면 그냥 원래값 넣기
                        rowData[key] = value?.ToString() ?? "";
                        continue;
                    }

                    if (type.EndsWith("[]")) // 배열 타입 처리
                    {
                        string elementType = type.Substring(0, type.Length - 2); // "int[]"=> "int"
                        List<object> list = new List<object>();

                        if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            string[] parts = value.ToString().Split(',');

                            foreach (string part in parts)
                            {
                                string trimmed = part.Trim();

                                switch (elementType)
                                {
                                    case "int":
                                        if (int.TryParse(trimmed, out int intVal))
                                            list.Add(intVal);
                                        break;
                                    case "float":
                                        if (float.TryParse(trimmed, out float floatVal))
                                            list.Add(floatVal);
                                        break;
                                    case "string":
                                        list.Add(trimmed);
                                        break;
                                    default:
                                        list.Add(trimmed); // 알 수 없는 타입은 string 처리
                                        break;
                                }
                            }
                        }

                        rowData[key] = list;
                    }
                    else
                    {
                        // 단일 값 처리
                        switch (type)
                        {
                            case "int":
                                if (value is double doubleValue)
                                {
                                    // 엑셀 숫자 타입은 double로 들어옴
                                    rowData[key] = (int)doubleValue;
                                }
                                else if (int.TryParse(value?.ToString(), out int intVal))
                                {
                                    rowData[key] = intVal;
                                }
                                else
                                {
                                    rowData[key] = 0;
                                }

                                break;

                            case "float":
                                if (value is double dVal)
                                {
                                    rowData[key] = (float)dVal;
                                }
                                else if (float.TryParse(value?.ToString(), out float fVal))
                                {
                                    rowData[key] = fVal;
                                }
                                else
                                {
                                    rowData[key] = 0f;
                                }

                                break;

                            case "string":
                                rowData[key] = value?.ToString() ?? "";
                                break;

                            default:
                                // 타입 몰라도 문자열로 넣기
                                rowData[key] = value?.ToString() ?? "";
                                break;
                        }
                    }
                }

                jsonData.Add(rowData);
            }

            return JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        }

        public static void SaveJson(string excelPath, string savePath)
        {
            string json = ConvertExcelToJson(excelPath);
            if (json == null)
            {
                Debug.LogError("엑셀을 JSON으로 변환하는 데 실패했습니다.");
                return;
            }

            File.WriteAllText(savePath, json);
            Debug.Log($"JSON 파일이 저장되었습니다: {savePath}");
        }
    }
}