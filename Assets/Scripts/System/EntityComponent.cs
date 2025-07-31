using System;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour
{
    private Component Root { get; set; }

    public T GetRoot<T>() where T : Component
    {
        return Root as T;
    }
    
    private void Reset()
    {
        if(false == TryGetComponent<EntityComponentContainer>(out EntityComponentContainer controller)) return;

        controller.PushComponent(this);
    }

    public virtual void Init<T>(T component ) where T : Component
    {
        Root = component;
    }

    public virtual void Dispose()
    {
    }

    public virtual void ManualUpdate()
    {
    }

    public virtual void ManualFixedUpdate()
    {
    }

    public virtual void OnPlay()
    {
    }

    public virtual void OnDead()
    {
    }
}
