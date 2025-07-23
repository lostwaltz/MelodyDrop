using System.Collections.Generic;
using UnityEngine;


namespace Engine
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> Bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => Bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => Bindings.Remove(binding);

        public static void Raise(T @event)
        {
            var snapshot = new HashSet<IEventBinding<T>>(Bindings);

            foreach (var binding in snapshot)
            {
                if (!Bindings.Contains(binding)) continue;
                
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        private static void Clear()
        {
            Bindings.Clear();
        }
    }
}