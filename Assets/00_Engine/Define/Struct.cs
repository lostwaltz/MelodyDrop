using System.Collections.Generic;
using System.Linq;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Engine
{
    public readonly struct AsyncOperationHandleGroup<T>
    {
        public AsyncOperationHandleGroup(List<AsyncOperationHandle<T>> handles)
        {
            _handles = handles;
        }
        
        private readonly List<AsyncOperationHandle<T>> _handles;

        public float Progress => _handles.Count == 0 ? 0 : _handles.Average(h => h.PercentComplete);
        public bool IsDone => _handles.Count == 0 || _handles.All(o => o.IsDone);

        public AsyncOperationHandleGroup(int initialCapacity)
        {
            _handles = new List<AsyncOperationHandle<T>>(initialCapacity);
        }
    }
}