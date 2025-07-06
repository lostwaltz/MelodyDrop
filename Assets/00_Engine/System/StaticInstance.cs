using System;

public interface IServiceRegistrable
{
    void RegisterService();
}

namespace Engine
{
    public abstract class StaticInstance<T> where T : class, new()
    {
        private static readonly Lazy<T> StaticIns = new Lazy<T>(() =>
        {
            var inst = new T();

            if (inst is IServiceRegistrable service)
            {
                service.RegisterService();
            }

            return inst;
        });

        public static T Instance => StaticIns.Value;
    }
}