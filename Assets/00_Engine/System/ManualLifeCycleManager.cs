using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class ManualLifeCycleManager : StaticInstance<ManualLifeCycleManager>, IServiceRegistrable
    {
        private readonly Queue<ManualMonoBehaviour> _awakeQueue = new();
        private readonly Queue<ManualMonoBehaviour> _startQueue = new();

        private bool _hasRunAwake = false;
        private bool _hasRunStart = false;
        
        public void BindAwake(ManualMonoBehaviour behaviour)
        {
            if (_hasRunAwake)
            {
                behaviour.ManualAwake();
                return;
            }

            _awakeQueue.Enqueue(behaviour);
        }

        public void BindStart(ManualMonoBehaviour behaviour)
        {
            if (_hasRunStart)
            {
                behaviour.ManualStart();
                return;
            }

            _startQueue.Enqueue(behaviour);
        }

        public void RunAwake()
        {
            _hasRunAwake = true;

            while (_awakeQueue.Count > 0)
                _awakeQueue.Dequeue().ManualAwake();
        }

        public void RunStart()
        {
            _hasRunStart = true;

            while (_startQueue.Count > 0)
                _startQueue.Dequeue().ManualStart();
        }

        public void RegisterService()
        {
            ServiceManager.Instance.Register(this);
        }
    }
}