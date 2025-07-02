using System;
using UnityEngine;


namespace Engine
{
    public abstract class UIBase : ManualMonoBehaviour
    {
        [SerializeField] private bool registerUI;

        private readonly Action _onOpenEvent;
        private readonly Action _onCloseEvent;

        public override void ManualAwake()
        {
            if (!registerUI) return;

            UIManager.Instance.RegisterUI(this);
        }

        // public static void BindEvent(GameObject go, Action<PointerEventData> action, EnumTypes.UIEvent type = EnumTypes.UIEvent.Click)
        // {
        //     UIEventHandler evt = go.GetOrAddComponent<UIEventHandler>();
        //
        //     switch (type)
        //     {
        //         default:
        //         case UIEvent.Click:
        //             evt.OnClickEvent -= action;
        //             evt.OnClickEvent += action;
        //             break;
        //         case UIEvent.Up:
        //             evt.OnUpEvent -= action;
        //             evt.OnUpEvent += action;
        //             break;
        //         case UIEvent.Down:
        //             evt.OnDownEvent -= action;
        //             evt.OnDownEvent += action;
        //             break;
        //         case UIEvent.Drag:
        //             evt.OnDragEvent -= action;
        //             evt.OnDragEvent += action;
        //             break;
        //         case UIEvent.BeginDrag:
        //             evt.OnBeginDragEvent -= action;
        //             evt.OnBeginDragEvent += action;
        //             break;
        //         case UIEvent.EndDrag:
        //             evt.OnEndDragEvent -= action;
        //             evt.OnEndDragEvent += action;
        //             break;
        //         case UIEvent.Enter:
        //             evt.OnEnterEvent -= action;
        //             evt.OnEnterEvent += action;
        //             break;
        //         case UIEvent.Exit:
        //             evt.OnExitEvent -= action;
        //             evt.OnExitEvent += action;
        //             break;
        //     }
        // }
        //
        //
        
        public virtual void Open()
        {
            gameObject.SetActive(true);
            _onOpenEvent?.Invoke();


            OpenProcedure();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            _onCloseEvent?.Invoke();

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