using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor
{
    private bool _showItemData = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DataManager dataManager = (DataManager)target;

        if (dataManager.ItemDataMap == null)
        {
            EditorGUILayout.HelpBox("ItemDataMap is null or not initialized.", MessageType.Warning);
            return;
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField($"Item Count: {dataManager.ItemDataMap.Count}", EditorStyles.boldLabel);

        _showItemData = EditorGUILayout.Foldout(_showItemData, "Show Item Data");

        if (!_showItemData) return;
        
        EditorGUI.indentLevel++;
        foreach (var kvp in dataManager.ItemDataMap)
        {
            var item = kvp.Value;
            if (item != null)
            {
                EditorGUILayout.LabelField($"[{kvp.Key}] {item.Name}");
            }
            else
            {
                EditorGUILayout.LabelField($"[{kvp.Key}] <null>");
            }
        }
        EditorGUI.indentLevel--;
    }
}