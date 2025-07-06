using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor
{
    private readonly Dictionary<string, bool> _foldouts = new();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DataManager dataManager = (DataManager)target;

        var fields = typeof(DataManager).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            // Dictionary<int, T> 필드만 필터링
            if (!field.FieldType.IsGenericType ||
                field.FieldType.GetGenericTypeDefinition() != typeof(Dictionary<,>) ||
                field.FieldType.GetGenericArguments()[0] != typeof(int)) continue;
            if (field.GetValue(dataManager) is not IDictionary dict)
            {
                EditorGUILayout.HelpBox($"{field.Name} is null or not initialized.", MessageType.Warning);
                continue;
            }

            _foldouts.TryAdd(field.Name, false);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"{field.Name} Count: {dict.Count}", EditorStyles.boldLabel);
            _foldouts[field.Name] = EditorGUILayout.Foldout(_foldouts[field.Name], $"Show {field.Name}");

            if (!_foldouts[field.Name]) continue;
            EditorGUI.indentLevel++;
            foreach (var key in dict.Keys)
            {
                object item = dict[key];
                if (item == null)
                {
                    EditorGUILayout.LabelField($"[{key}] <null>");
                    continue;
                }

                EditorGUILayout.LabelField($"[{key}] {item.GetType().Name}", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                DrawObjectFields(item);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
    }

    private void DrawObjectFields(object obj)
    {
        var type = obj.GetType();

        // public 필드 출력
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        foreach (var field in fields)
        {
            object value = field.GetValue(obj);
            EditorGUILayout.LabelField($"{field.Name}: {FormatValue(value)}");
        }

        // public 프로퍼티 출력 (읽기 전용만)
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var prop in properties)
        {
            if (prop.GetIndexParameters().Length > 0 || !prop.CanRead)
                continue;

            object value;
            try { value = prop.GetValue(obj); }
            catch { continue; }

            EditorGUILayout.LabelField($"{prop.Name}: {FormatValue(value)}");
        }
    }

    private string FormatValue(object value)
    {
        if (value == null)
            return "<null>";

        var type = value.GetType();

        // 배열
        if (!type.IsArray)
            return value switch
            {
                // List<T>, IEnumerable
                System.Collections.IEnumerable enumerable when !(value is string) => FormatEnumerable(enumerable),
                // 문자열 따옴표 추가
                string str => $"\"{str}\"",
                _ => value.ToString()
            };
        var array = value as System.Array;
        return FormatEnumerable(array);

    }

    private static string FormatEnumerable(System.Collections.IEnumerable enumerable)
    {
        if (enumerable == null) return "<null>";

        List<string> elements = new();
        foreach (var element in enumerable)
        {
            elements.Add(element?.ToString() ?? "null");
        }

        // 너무 많으면 자르기
        const int maxPreview = 10;
        return elements.Count > maxPreview ? $"[{string.Join(", ", elements.Take(maxPreview))}, ...]" : $"[{string.Join(", ", elements)}]";
    }
}
