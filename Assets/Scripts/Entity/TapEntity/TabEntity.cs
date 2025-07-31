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
    
    public TabEntitySpawner Spawner { get; set; }

    public ColorData ColorData { get; private set; }

    [SerializeField] private EntityComponentContainer container;
    [SerializeField] private InstanceMaterialCreator instanceMaterialCreator;
    [SerializeField] private BaseMove move;

    [SerializeField] private PointEventHub hub;

    public EventHub<EntityEvent> EventHub { get; private set; }
    
    public override void ManualAwake()
    {
        EventHub = new EventHub<EntityEvent>();

        container.Initialize(this);
    }
    
    public override void ManualStart()
    {
        container.RunOnPlay();
        
        hub.Bind(gameObject, PointEventType.Click, (_) => ShortCut.Get<GameInteractionHandler>().SelectCube(this));
    }

    public bool CheckMatch()
    {
        return Spawner.GlowFrame.colorKey == ColorData.ItemID;
    }

    public void OnMatchSuccess()
    {
        move.ChangeSpeed(5f);
    }

    public void ChangeColor(ColorData colorData)
    {
        if(colorData == null) return;
        
        ColorData = colorData;
        ChangeColor(colorData.GetColor());
    }
    
    private void ChangeColor(Color color)
    {
         instanceMaterialCreator.ChangeColor(color);
    }
}