using System;

namespace Engine
{
    public abstract class StaticInstance<T> where T : class, new()
    {
        private static readonly Lazy<T> StaticIns = new Lazy<T>(() => new T());

        public static T Instance => StaticIns.Value;

        protected StaticInstance() { }
    }
}