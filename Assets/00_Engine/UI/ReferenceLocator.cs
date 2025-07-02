using System.Collections.Generic;

namespace Engine
{
    public class ReferenceLocator : Singleton<ReferenceLocator>
    {
        private readonly Dictionary<string, IIdentifiable> _instances = new();

        public void Register(IIdentifiable instance)
        {
            string key = instance.GetIdentifier();
            _instances[key] = instance;
        }

        public T Get<T>(string key) where T : class, IIdentifiable
        {
            if (_instances.TryGetValue(key, out var instance))
            {
                return instance as T;
            }
            return null;
        }
    }
}