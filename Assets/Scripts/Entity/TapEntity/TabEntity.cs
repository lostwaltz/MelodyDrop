using Engine;
using UnityEngine;
using UnityEngine.Serialization;

public class TabEntity : ManualMonoBehaviour
{
    public enum EntityEvent
    {
        EntityDestroy
    }
    
    private EntityData _data;
    private ColorData _colorData;

    [SerializeField] private EntityComponentController controller;
    [SerializeField] private InstanceMaterialCreator instanceMaterialCreator;

    [SerializeField] private PointEventHub hub;

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
        
        hub.Bind(gameObject, PointEventType.Click, (_) => ShortCut.Get<GameInteractionHandler>().SelectCube(this));
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
}