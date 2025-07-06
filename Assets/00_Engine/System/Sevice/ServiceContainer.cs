using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class ServiceContainer
    {
        private readonly Dictionary<Type, object> _services = new();

        public bool TryGet<T>(out T service) where T : class
        {
            var type = typeof(T);
            
            if (_services.TryGetValue(type, out object obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            
            return false;
        }

        public T Get<T>() where T : class
        {
            var type = typeof(T);
            
            if (_services.TryGetValue(type, out object obj)) return obj as T;
            
            throw new ArgumentException($"Service of type {type.FullName} not registered");
        }

        public ServiceContainer Register<T>(T service, bool overrideValue = false) where T : class
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service), $"service of type {typeof(T).FullName} is null");
            
            var type = typeof(T);

            if (false == overrideValue)
            {
                if(!_services.TryAdd(type, service))
                    Debug.LogWarning($"Service of type {type.FullName} already registered");
            }
            else
            {
                _services[type] = service;
            }

            return this;
        }
    }
}