using Engine;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointClick : PointEventBase, IPointerClickHandler
{
    public override void ManualAwake()
    {
        base.ManualAwake();

        root.Register(PointEventType.Click, this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointEvent?.Invoke(eventData);
    }
}
