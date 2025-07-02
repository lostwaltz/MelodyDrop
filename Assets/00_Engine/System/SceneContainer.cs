using System;
using System.Collections.Generic;
using Engine;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scene Container", menuName = "New Scene Container")]
public class SceneContainer : ScriptableObject
{
    public List<SceneData> scenes;
    
    public Dictionary<string, SceneData> SceneDictionary;

    private void OnEnable()
    {
        SceneDictionary = new Dictionary<string, SceneData>();

        foreach (SceneData scene in scenes)
            SceneDictionary[scene.Name] = scene;
    }

    public SceneData GetScene(string sceneName)
    {
        SceneDictionary.TryGetValue(sceneName, out SceneData scene);
        
        if(scene == null) 
            Debug.LogWarning("Scene not found: " + sceneName);
        
        return scene;
    }
}
