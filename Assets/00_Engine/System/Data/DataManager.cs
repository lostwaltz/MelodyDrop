using System.Collections.Generic;
using Engine;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    private DataBase<ColorData> _colorDataBase;
    private DataBase<EntityData> _entityDataBase;
    private DataBase<SpawnPatternData> _patternDataBase;
    
    public Dictionary<int, ColorData> ColorDataMap = new();
    public Dictionary<int, EntityData> EntityDataMap = new();
    public Dictionary<int, SpawnPatternData> SpawnPatternDataMap = new();
    
    public override void InitializeSingleton()
    {
        base.InitializeSingleton();
        
        _colorDataBase = new DataBase<ColorData>(Util.Str.Clear().Append("Json/ColorDataBase").ToString());
        foreach (var data in _colorDataBase.DataBaseList)
            ColorDataMap[data.itemID] = data;
        
        _entityDataBase = new DataBase<EntityData>(Util.Str.Clear().Append("Json/EntityDataBase").ToString());
        foreach (var data in _entityDataBase.DataBaseList)
            EntityDataMap[data.itemID] = data;
        
        _patternDataBase = new DataBase<SpawnPatternData>(Util.Str.Clear().Append("Json/SpawnPatternDataBase").ToString());
        foreach (var data in _patternDataBase.DataBaseList)
            SpawnPatternDataMap[data.itemID] = data;
    }
}
