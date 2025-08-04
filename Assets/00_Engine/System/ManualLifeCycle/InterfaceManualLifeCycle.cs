using System;
using Engine;
using Mono.Cecil;
using UnityEngine;

public interface IManualLifeCycle
{
    public void ManualAwake();
    public void ManualStart();
    public void ManualDestroy();
}

public abstract class ManualMonoBehaviour : MonoBehaviour, IManualLifeCycle
{
    private void Awake()
    {
        ManualLifeCycleManager.Instance.BindAwake(this);
        
        ManualLifeCycleManager.Instance.BindDestroy(this, this);
    }

    private void Start()
    {
        ManualLifeCycleManager.Instance.BindStart(this);
    }

    public virtual void ManualAwake()
    {
    }
    public virtual void ManualStart()
    {
    }
    public virtual void ManualDestroy()
    {
        
    }
    
    public void Destroy(GameObject go) => throw new InvalidOperationException("use manual_life_cycleManager manualDestroy method");

    public void ManualDestroy(ManualMonoBehaviour component)
    {
        Core.GetSingleton<ManualLifeCycleManager>().ManualDestroy(component);
    }
}