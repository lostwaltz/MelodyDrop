using Engine;
using UnityEngine;

public class TabEntity : ManualMonoBehaviour
{
    public enum EntityEvent
    {
        EntityDestroy
    }
    
    private TabEntityData _data;

    [SerializeField] private EntityComponentController controller;
    
    [SerializeField] private InstanceMaterialCreator instanceMaterialCreator;

    public EventHub<EntityEvent> EventHub { get; private set; }
    
    public override void ManualAwake()
    {
        EventHub = new EventHub<EntityEvent>();
        
        controller.Initialize();
    }

    public override void ManualStart()
    {
        base.ManualStart();
        
        controller.RunOnPlay();
    }

    public void ChangeColor(Color color)
    {
        instanceMaterialCreator.ChangeColor(color);
    }
}