using System;
using Engine;
using Mono.Cecil;
using UnityEngine;

public interface IManualLifeCycle
{
}

public interface IManualAwake : IManualLifeCycle
{
    public void ManualAwake();
}

public interface IManualStart : IManualLifeCycle
{
    public void ManualStart();
}

public interface IManualDestroy : IManualLifeCycle
{
    public void ManualDestroy();
}

public abstract class ManualMonoBehaviour : MonoBehaviour, IManualAwake, IManualStart, IManualDestroy
{
    public bool HasDestroyed { get; set; } = false;
    
    private void Awake()
    {
        ManualLifeCycleManager.Instance.BindAwake(this);
        
        ManualLifeCycleManager.Instance.BindDestroy(this, this);
    }

    private void Start()
    {
        ManualLifeCycleManager.Instance.BindStart(this);
    }

    private void OnDestroy()
    {
        ManualLifeCycleManager.Instance.ManualDestroy(this);
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
}