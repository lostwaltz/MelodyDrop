using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Engine
{
    public class UIBase : ManualMonoBehaviour
    {
        public event Action OnOpenEvent;
        public event Action OnCloseEvent;

        private UIEventHub _eventHub;
        public UIEventHub EventHub 
        {   
            get
            {
                _eventHub ??= new UIEventHub();

                return _eventHub; 
            } 
        }

        public override void ManualAwake()
        {
            Core.GetService<UIManager>().RegisterUI(this);
        }

        public void BindEvent(GameObject go, Action<PointerEventData> action, PointEventType type = PointEventType.Click)
        {
            EventHub?.Bind(go, type, action);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
            OnOpenEvent?.Invoke();

            OpenProcedure();
        }
        public virtual void Close()
        {
            gameObject.SetActive(false);
            OnCloseEvent?.Invoke();

            CloseProcedure();
        }
        protected virtual void OpenProcedure()
        {
        }
        protected virtual void CloseProcedure()
        {

        }
    }
}