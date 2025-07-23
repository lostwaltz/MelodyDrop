using Engine;
using UnityEngine;

public class TabEntity : ManualMonoBehaviour, IInteractable
{
    public enum EntityEvent
    {
        EntityDestroy
    }
    
    private EntityData _data;
    private ColorData _colorData;

    [SerializeField] private EntityComponentController controller;
    
    [SerializeField] private InstanceMaterialCreator instanceMaterialCreator;

    public EventHub<EntityEvent> EventHub { get; private set; }
    
    public override void ManualAwake()
    {
        EventHub = new EventHub<EntityEvent>();

        InteractionType = InteractionType.Cube;
        
        controller.Initialize();
    }

    public override void ManualStart()
    {
        base.ManualStart();
        
        controller.RunOnPlay();
    }

    public void ChangeColor(ColorData colorData)
    {
        if(colorData == null) return;
        
        _colorData = colorData;
        ChangeColor(colorData.GetColor());
    }
    
    private void ChangeColor(Color color)
    {
         instanceMaterialCreator.ChangeColor(color);
    }

    public InteractionType InteractionType { get; set; }
    public void Interact()
    {
        
    }
}