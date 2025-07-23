using System;
using UnityEngine;

[Serializable]
public class SpawnPatternData
{
    public int ItemID;
    public string Name;
    public int[] EntityKeyArray;
    public float[] SpawnDelayArray;
    public int[] EntityColorKey;
    public int[] SpawnIndexArray;
}
