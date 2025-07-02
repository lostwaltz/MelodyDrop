using UnityEditor;
using UnityEngine;
using System.IO;

namespace Engine
{
    [CustomEditor(typeof(JsonParser))]
    public class JsonParserInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Json From Excel Files"))
            {
                JsonParser parser = (JsonParser)target;
                GenerateJsonFromAllExcelFiles(parser.excelFolder, parser.jsonSaveFolder);
            }
        }

        private void GenerateJsonFromAllExcelFiles(string excelFolder, string jsonSaveFolder)
        {
            if (!Directory.Exists(excelFolder))
            {
                Debug.LogError($"엑셀 폴더가 없습니다: {excelFolder}");
                return;
            }

            if (!Directory.Exists(jsonSaveFolder))
            {
                Directory.CreateDirectory(jsonSaveFolder);
                Debug.Log($"JSON 저장 폴더 생성: {jsonSaveFolder}");
            }

            string[] excelFiles = Directory.GetFiles(excelFolder, "*.xls*");

            if (excelFiles.Length == 0)
            {
                Debug.LogWarning("엑셀 파일을 찾지 못했습니다.");
                return;
            }

            foreach (var filePath in excelFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string savePath = Path.Combine(jsonSaveFolder, fileName + ".json");

                ExcelToJsonConverter.SaveJson(filePath, savePath);
            }

            AssetDatabase.Refresh();
            Debug.Log("엑셀 → JSON 변환 작업 완료");
        }
    }
}