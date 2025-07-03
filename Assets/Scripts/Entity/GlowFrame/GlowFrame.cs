using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GlowFrame : ManualMonoBehaviour
{
    private static readonly string EmColor = "_Color";
    [SerializeField] private Renderer rend;

    [SerializeField] private Color color;
    
    public void Awake()
    {
        Material instancedMat = new Material(rend.material);
        instancedMat.SetColor(EmColor, color * 10f);
        
        rend.material = instancedMat;
    }
}
