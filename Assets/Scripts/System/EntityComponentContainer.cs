using System.Collections.Generic;
using UnityEngine;

public class EntityComponentContainer : MonoBehaviour
{
    [SerializeField] private List<EntityComponent> components;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        
        components.Clear();
        var comps = GetComponentsInChildren<EntityComponent>(true);
        components.AddRange(comps);
#endif
    }
    
    public void Initialize<T>(T root) where T : Component
    {
        foreach (var component in components)
        {
            component.Init(root);
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
