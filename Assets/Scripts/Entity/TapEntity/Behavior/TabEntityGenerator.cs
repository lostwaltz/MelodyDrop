using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum MeshColor
{
    Red, Orange, Yellow, Green, Blue
}

public class TabEntityGenerator : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private TabEntity tabEntity;

    [SerializeField] private float spawnPointY;
    
    private readonly Dictionary<MeshColor, Vector3> _spawnPoints = new();

    private void Start()
    {
        for (int i = 0; i < Enum.GetValues(typeof(MeshColor)).Length; i++)
        {
            _spawnPoints[(MeshColor)i] = new Vector3(-2.4f, spawnPointY, 0) + (new Vector3(1.2f, 0, 0) * i);

            CreateEntity((MeshColor)i);
        }
    }

    private void CreateEntity(MeshColor color)
    {
        TabEntity entity = Instantiate(tabEntity);
            
        entity.ChangeColor(colors[(int)color]);
        entity.transform.position = _spawnPoints[color];
            
        entity.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
