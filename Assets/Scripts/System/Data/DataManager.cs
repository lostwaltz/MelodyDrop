using System.Collections.Generic;
using Engine;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private DataBase<TabEntityData> _entityData;

    public readonly Dictionary<int, TabEntityData> ItemDataMap = new();
    
    protected override void Awake()
    {
        base.Awake();
        
        _entityData = new DataBase<TabEntityData>("Json/MeshDataBase");
        foreach (var data in _entityData.DataBaseList)
        {
            ItemDataMap.Add(data.ItemID, data);
        }
    }
}
