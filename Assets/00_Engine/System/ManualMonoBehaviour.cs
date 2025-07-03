using System;
using Engine;
using UnityEngine;

public abstract class ManualMonoBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        if(false == enabled || false == gameObject.activeSelf) return;
        
        ManualLifeCycleManager.Instance.BindAwake(this);
    }

    private void Start()
    {
        if(false == enabled || false == gameObject.activeSelf) return;
        
        ManualLifeCycleManager.Instance.BindStart(this);
    }

    public virtual void ManualEnable() { }
    public virtual void ManualAwake() { }
    public virtual void ManualStart() { }
    public virtual void ManualFixedUpdate() { }
    public virtual void ManualUpdate() { }
    public virtual void ManualDisable() { }
    public virtual void ManualDestroy() { }
}
