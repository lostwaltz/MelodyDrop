using System.Collections.Generic;
using Engine;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private DataBase<EntityData> _entityData;
    private DataBase<SpawnPatternData> _spawnPatternData;
    private DataBase<ColorData> _colorData;

    public readonly Dictionary<int, EntityData> ItemDataMap = new();
    public readonly Dictionary<int, SpawnPatternData> SpawnPatternDataMap = new();
    public readonly Dictionary<int, ColorData> ColorDataMap = new();
    
    
    protected override void Awake()
    {
        base.Awake();
        
        _entityData = new DataBase<EntityData>("Json/EntityDataBase");
        foreach (var data in _entityData.DataBaseList)
        {
            ItemDataMap.Add(data.ItemID, data);
        }
        
        _spawnPatternData = new DataBase<SpawnPatternData>("Json/SpawnPatternDataBase");
        foreach (var data in _spawnPatternData.DataBaseList)
        {
            SpawnPatternDataMap.Add(data.ItemID, data);
        }
        
        _colorData = new DataBase<ColorData>("Json/ColorDataBase");
        foreach (var data in _colorData.DataBaseList)
        {
            ColorDataMap.Add(data.ItemID, data);
        }
    }
}
