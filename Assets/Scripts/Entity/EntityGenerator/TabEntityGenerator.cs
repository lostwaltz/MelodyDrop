using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Engine;
using UnityEngine;
using Random = UnityEngine.Random;

public enum MeshColor
{
    Red, Orange, Yellow, Green, Blue
}

public class TabEntityGenerator : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private TabEntity tabEntity;
    [SerializeField] private TabEntitySpawner[] spawners;

    public readonly ServiceContainer ServiceContainer = new();

    public void Generate(SpawnPatternData spawnPatternData)
    {
        StartCoroutine(SpawnRoutine(spawnPatternData));
    }
    
    private void SpawnEntity(int entityKey, MeshColor color, int positionIndex)
    {
        if(-1 == positionIndex)
            positionIndex = Random.Range(0, spawners.Length);

        bool isSpawn = spawners[positionIndex].SpawnEntity(entityKey, colors[ColorToInt(color)]);
        
        if (isSpawn) 
            return;
        
        List<TabEntitySpawner> spawnerList = spawners.Where(spawner => spawner.CanSpawn).ToList();

        int randIndex = Random.Range(0, spawnerList.Count);
            
        spawnerList[randIndex].SpawnEntity(entityKey, colors[ColorToInt(color)]);
    }

    private IEnumerator SpawnRoutine(SpawnPatternData pattern)
    {
        int length = pattern.EntityKeyArray.Length;

        for (int i = 0; i < length; i++)
        {
            int entityKey = pattern.EntityKeyArray[i];
            float delay = pattern.SpawnDelayArray[i];
            int color = pattern.EntityColorArray[i];
            int posIndex = pattern.SpawnIndexArray[i];

            yield return new WaitForSeconds(delay);
            
            SpawnEntity(0, IntToColor(color), posIndex);
        }
    }

    public MeshColor IntToColor(int index)
    {
        return index switch
        {
            -1 => (MeshColor)Random.Range(0, 5),
            0 => MeshColor.Red,
            1 => MeshColor.Orange,
            2 => MeshColor.Yellow,
            3 => MeshColor.Green,
            4 => MeshColor.Blue,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
        };
    }
    private int ColorToInt(MeshColor color)
    {
        return color switch
        {
            MeshColor.Red => 0,
            MeshColor.Orange => 1,
            MeshColor.Yellow => 2,
            MeshColor.Green => 3,
            MeshColor.Blue => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}
