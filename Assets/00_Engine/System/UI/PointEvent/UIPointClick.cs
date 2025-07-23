using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine
{

    public class UIPointClick : UIPointEventBase, IPointerClickHandler
    {
        public override void ManualAwake()
        {
            base.ManualAwake();

            _root.EventHub.Register(PointEventType.Click, this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointEvent(eventData);
        }
    }
}