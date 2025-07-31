using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Engine;
using UnityEngine;
using Random = UnityEngine.Random;

public class TabEntityGenerator : MonoBehaviour
{
    [SerializeField] private TabEntitySpawner[] spawners;

    public readonly ServiceContainer ServiceContainer = new();

    public void Generate(SpawnPatternData spawnPatternData)
    {
        StartCoroutine(SpawnRoutine(spawnPatternData));
    }
    
    private void SpawnEntity(int entityKey, int colorKey, int positionIndex)
    {
        if(-1 == positionIndex)
            positionIndex = Random.Range(0, spawners.Length);

        bool isSpawn = spawners[positionIndex].SpawnEntity(entityKey, colorKey);
        
        if (isSpawn) 
            return;
        
        List<TabEntitySpawner> spawnerList = spawners.Where(spawner => spawner.CanSpawn).ToList();

        int randIndex = Random.Range(0, spawnerList.Count);
            
        spawnerList[randIndex].SpawnEntity(entityKey, colorKey);
    }

    private IEnumerator SpawnRoutine(SpawnPatternData pattern)
    {
        int length = pattern.EntityKeyArray.Length;
        
        List<int> colorKeyList = new List<int> { 100000, 100001, 100002, 100003, 100004 };
        
        for (int i = 0; i < length; i++)
        {
            int entityKey = pattern.EntityKeyArray[i];
            float delay = pattern.SpawnDelayArray[i];
            int colorKey = pattern.EntityColorKey[i];
            int posIndex = pattern.SpawnIndexArray[i];

            yield return new WaitForSeconds(delay);

            if (colorKey == -1)
            {
                int index = Random.Range(0, colorKeyList.Count);
                colorKey = colorKeyList[index];
                colorKeyList.RemoveAt(index);
            }
            
            SpawnEntity(0, colorKey, posIndex);
        }
    }
}
