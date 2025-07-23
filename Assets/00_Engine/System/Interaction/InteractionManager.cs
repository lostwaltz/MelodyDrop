using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Engine
{
    public class InteractionManager : Singleton<InteractionManager>
    {
        [SerializeField]
        private InteractionType filter = InteractionType.All;
        
        private readonly EventHub<InteractionType, IInteractable> _eventHub = new();

        private void TryInteract()
        {
            // if (EventSystem.current.IsPointerOverGameObject())
            //     return;

            if (Camera.main == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable == null) return;
            
            if ((filter & interactable.InteractionType) == 0) return;

            _eventHub.Publish(interactable.InteractionType, interactable);
        }

        public void Subscribe(InteractionType type, Action<IInteractable> action)
            => _eventHub.Subscribe(type, action);

        public void Unsubscribe(InteractionType type, Action<IInteractable> action)
            => _eventHub.Unsubscribe(type, action);

        public override void InitializeSingleton()
        {
            base.InitializeSingleton();
            
            InputManager.Instance.Subscribe(InputTrigger.Click, TryInteract);
        }
    }

}