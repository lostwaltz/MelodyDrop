using Engine;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine
{
    public abstract class PointEventBase : ManualMonoBehaviour
    {
        [SerializeField] protected PointEventHub root;
        public Action<PointerEventData> OnPointEvent { get; set; }
        public PointEventType EventType { get; set; }

        public override void ManualAwake()
        {
            if (root == null)
            {
                Transform transformRoot = gameObject.transform;

                while (transformRoot.parent != null)
                    transformRoot = transformRoot.parent;

                root = transformRoot.GetComponent<PointEventHub>() ??
                       transformRoot.GetComponentInChildren<PointEventHub>();
                if (root == null)
                {
                    Log.Warn($"{name} can't find PointEventContainer.");
                    return;
                }
            }
            
            root.Register(PointEventType.Click, this);
        }

        public override void ManualDestroy()
        {
            root.Unregister(EventType, this);
        }

        protected void Reset()
        {
            Transform transformRoot = gameObject.transform;
            while (transformRoot.parent != null)
                transformRoot = transformRoot.parent;

            root = transformRoot.GetComponent<PointEventHub>() ??
                   transformRoot.GetComponentInChildren<PointEventHub>();
            ;

            if (root == null)
                Log.Warn($"{name} can't find PointEventContainer.");
        }
    }
}