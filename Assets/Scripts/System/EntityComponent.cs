using System;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour
{
    private void Reset()
    {
        if(false == TryGetComponent<EntityComponentController>(out EntityComponentController controller)) return;

        controller.PushComponent(this);
    }

    public virtual void Init()
    {
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
