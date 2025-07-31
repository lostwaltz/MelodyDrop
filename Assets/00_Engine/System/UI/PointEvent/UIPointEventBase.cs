using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Engine
{
    public abstract class UIPointEventBase : ManualMonoBehaviour
    {
        [SerializeField] protected UIBase root;

        public override void ManualAwake()
        {
            if (root == null)
            {
                Transform transformRoot = transform.root;
                this.root = transformRoot.GetComponent<UIBase>() ?? transformRoot.GetComponentInChildren<UIBase>();
                if (this.root == null)
                {
                    Log.Warn($"{name} UIBase");
                    return;
                }
            }

            root.EventHub.Register(PointEventType.Click, this);
        }

        protected void Reset()
        {
            Transform transformRoot = gameObject.transform;
            while (transformRoot.parent != null)
            {
                transformRoot = transformRoot.parent;
            }

            this.root = transformRoot.GetComponent<UIBase>();
            this.root ??= transformRoot.GetComponentInChildren<UIBase>();

            if (this.root == null)
                Log.Warn($"{this.name}");
        }

        public Action<PointerEventData> OnPointEvent { get; set; }

    }
}