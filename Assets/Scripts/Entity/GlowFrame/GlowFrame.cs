using System;
using Engine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class GlowFrame : ManualMonoBehaviour
{
    private static readonly string EmColor = "_Color";
    [SerializeField] private Renderer rend;
    [SerializeField] private Color color;
    [SerializeField] private float pow = 10f;
    
    [SerializeField] public int colorKey;
    [SerializeField] public EventTrigger trigger;
    
    public void Awake()
    {
        Material instancedMat = new Material(rend.material);
        instancedMat.SetColor(EmColor, color * pow);
        
        rend.material = instancedMat;

    }
}
