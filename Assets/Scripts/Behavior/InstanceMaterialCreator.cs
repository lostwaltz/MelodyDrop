using System;
using UnityEngine;

public class InstanceMaterialCreator : EntityComponent
{
    [SerializeField] private Material instancedMat;

    public override void Init()
    {
        Renderer rend = GetComponent<Renderer>();
        instancedMat = new Material(rend.material);
        
        rend.material = instancedMat;
    }

    public void ChangeColor(Color color)
    {
        instancedMat.color = color;
    }
}
