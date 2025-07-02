using UnityEngine;

public class EntityComponentController : MonoBehaviour
{
    [SerializeField] private EntityComponent[] components;

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
}
