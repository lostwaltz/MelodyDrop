using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class ManualLifeCycleManager : StaticInstance<ManualLifeCycleManager>, IServiceRegistrable
    {
        private readonly Queue<IManualAwake> _manualAwakes = new();
        private readonly Queue<IManualStart> _manualStarts= new();
        private readonly Dictionary<Component, IManualDestroy> _manualDestroys = new();
        
        private bool _hasRunAwake = false;
        private bool _hasRunStart = false;
        private bool _hasRunDestroy = false;

        public void BindAwake(IManualAwake behaviour)
        {
            if (_hasRunAwake)
            {
                behaviour.ManualAwake();
                return;
            }
            
            _manualAwakes.Enqueue(behaviour);
        }
        
        public void BindStart(IManualStart behaviour)
        {
            if (_hasRunStart)
            {
                behaviour.ManualStart();
                return;
            }
            
            _manualStarts.Enqueue(behaviour);
        }
        
        public void BindDestroy(Component component, IManualDestroy behaviour)
        {
            if (_hasRunDestroy)
            {
                behaviour.ManualDestroy();
                return;
            }
            
            _manualDestroys[component] = behaviour;
        }

        private void UnbindDestroy(Component component)
        {
            _manualDestroys.Remove(component);
        }

        public void RunAwake()
        {
            _hasRunAwake = true;

            while (_manualAwakes.Count > 0)
                _manualAwakes.Dequeue().ManualAwake();
        }

        public void RunStart()
        {
            _hasRunStart = true;

            while (_manualStarts.Count > 0)
                _manualStarts.Dequeue().ManualStart();
        }

        public void RunDestroy()
        {
            _hasRunDestroy = true;

            foreach (var behaviour in _manualDestroys)
                behaviour.Value.ManualDestroy();
            
            _manualDestroys.Clear();
        }

        public void ManualDestroy(ManualMonoBehaviour component)
        {
            if(true == component.HasDestroyed)
                return;
            
            component.HasDestroyed = true;
            
            component.ManualDestroy();
            UnbindDestroy(component);
        }

        public void RegisterService()
        {
            ServiceManager.Instance.Register(this);
        }
    }
}