using System;
using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class TabEntityGenerator : MonoBehaviour
{
    [SerializeField] private List<TabEntitySpawner> tabEntitySpawnerList = new(); 
    
    private void Reset()
    {
        TabEntitySpawner[] children = gameObject.GetComponentsInChildren<TabEntitySpawner>();

        tabEntitySpawnerList.Clear();
        tabEntitySpawnerList.AddRange(children);
    }

    private void Awake()
    {
        GameManager.Instance.OnSpawn += (data) => GenerateTabEntity(data.Data, data.OnGenerateComplete);
    }

    public void GenerateTabEntity(SpawnPatternData spawnPatternData, Action onGenerateComplete)
    {
        StartCoroutine(GenerateRoutine(spawnPatternData, onGenerateComplete));
    }
    
    private IEnumerator GenerateRoutine(SpawnPatternData spawnPatternData, Action onGenerateComplete)
    {
        ResetGenerate();

        for (int i = 0; i < spawnPatternData.entityKeyArray.Length; i++)
        {
            float delay = 0f;
            if (spawnPatternData.spawnDelayArray != null && i < spawnPatternData.spawnDelayArray.Length)
                delay = spawnPatternData.spawnDelayArray[i];

            if (delay > 0f)
                yield return new CustomWaitForSec(delay, TimeType.GameObject);

            int index = -1;
            if (spawnPatternData.spawnIndexArray != null && i < spawnPatternData.spawnIndexArray.Length)
                index = spawnPatternData.spawnIndexArray[i];

            EntityData entityData = DataManager.Instance.EntityDataMap[spawnPatternData.entityKeyArray[i]];
            ColorData  colorData = null;
            int colorKey = (spawnPatternData.entityColorKey != null && i < spawnPatternData.entityColorKey.Length) 
                ? spawnPatternData.entityColorKey[i] : -1;
            if (colorKey != -1)
                colorData = DataManager.Instance.ColorDataMap[colorKey];

            if (index < 0)
            {
                GenerateTabEntity(entityData, colorData);
            }
            else
            {
                GenerateTabEntity(entityData, colorData, index);
            }
        }

        onGenerateComplete?.Invoke();
    }

    private void GenerateTabEntity(EntityData entityData, ColorData colorData)
    {
        List<TabEntitySpawner> canSpawnList = tabEntitySpawnerList.FindAll(spawner => spawner.CanSpawn);
        if (canSpawnList.Count == 0)
            return;

        int rand = UnityEngine.Random.Range(0, canSpawnList.Count);
        TabEntitySpawner target = canSpawnList[rand];
        target.SpawnEntity(entityData, colorData);
        target.CanSpawn = false;
    }

    private void GenerateTabEntity(EntityData entityData, ColorData colorData, int index)
    {
        if(index < 0 || index >= tabEntitySpawnerList.Count)
            return;
        
        TabEntitySpawner target = tabEntitySpawnerList[index];
        target.SpawnEntity(entityData, colorData);
        target.CanSpawn = false;
    }

    private void ResetGenerate()
    {
        foreach (var spawner in tabEntitySpawnerList)
        {
            spawner.CanSpawn = true;
        }
    }
}
