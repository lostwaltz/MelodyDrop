using System.Collections.Generic;
using UnityEngine;

public class EntityComponentController : MonoBehaviour
{
    [SerializeField] private List<EntityComponent> components;

    public void Initialize()
    {
        foreach (var component in components)
        {
            component.Init();
        }
    }

    public void RunOnPlay()
    {
        foreach (var component in components)
        {
            component.OnPlay();
        }
    }

    public void PushComponent(EntityComponent component)
    {
        components.Add(component);
    }
}
