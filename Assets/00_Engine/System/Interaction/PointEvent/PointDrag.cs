using Engine;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointDrag : PointEventBase, IDragHandler
{
    public override void ManualAwake()
    {
        base.ManualAwake();

        root.Register(PointEventType.Drag, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnPointEvent?.Invoke(eventData);
    }
}
