using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Engine
{
    public enum InputTrigger
    {
        Click,
    }
    
    public class InputManager : Singleton<InputManager>
    {
        private InputSystem_Actions _input;
        private InputSystem_Actions.PlayerActions _actions;

        private readonly EventHub<InputTrigger> _eventHub = new();

        public override void InitializeSingleton()
        {
            base.InitializeSingleton();

            _input = new InputSystem_Actions();
            _input.Enable();
            
            _actions = _input.Player;
            _actions.Click.performed += _ => _eventHub.Publish(InputTrigger.Click);
        }

        public void Subscribe(InputTrigger trigger, Action action)
            => _eventHub.Subscribe(trigger, action);
    }
}