using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class DataBase<T> where T : class
{
    public List<T> DataBaseList { get; private set; }

    public DataBase(List<T> list)
    {
        GenerateDbFromList(list);
    }
    public DataBase(string path = "JSON/")
    {
      
        var json = Resources.Load<TextAsset>(path).text;
        
        DataBaseList = JsonConvert.DeserializeObject<List<T>>(json);
    }

    private void GenerateDbFromList(List<T> list)
    {
        DataBaseList = new List<T>(list);
    }
}