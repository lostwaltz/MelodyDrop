using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class ServiceManager : StaticInstance<ServiceManager>
    {
        private readonly ServiceContainer _globalContainer = new();

        public T Get<T>() where T : class => _globalContainer.Get<T>();
        public void Register<T>(T service) where T : class => _globalContainer.Register(service);
    }
}
