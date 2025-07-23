using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine
{
    public abstract class UIPointEventBase : ManualMonoBehaviour
    {
        [SerializeField] protected UIBase _root;

        public override void ManualAwake()
        {
            if (_root == null)
            {
                Transform root = transform.root;
                _root = root.GetComponent<UIBase>() ?? root.GetComponentInChildren<UIBase>();
                if (_root == null)
                {
                    Log.Warn($"{name} �̺�Ʈ ��� ����: UIBase�� ã�� �� ����.");
                    return;
                }
            }

            _root.EventHub.Register(PointEventType.Click, this);
        }

        protected void Reset()
        {
            Transform root = gameObject.transform;
            while (root.parent != null)
            {
                root = root.parent;
            }

            _root = root.GetComponent<UIBase>();
            _root ??= root.GetComponentInChildren<UIBase>();

            if (_root == null)
                Log.Warn($"{this.name} �� ����� ����� �����ϴ�.");
        }

        public Action<PointerEventData> OnPointEvent { get; set; }

    }
}