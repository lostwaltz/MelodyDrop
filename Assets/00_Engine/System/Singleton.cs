using System;
using System.Linq;
using UnityEngine;

namespace Engine
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static bool HasInstance => _instance != null;
        private float InitializationTime { get; set; }
        
        public static T Instance
        {
            get
            {
                if (HasInstance) return _instance;

                _instance = FindAnyObjectByType<T>();
            
                if (HasInstance) return _instance;

                var go = new GameObject
                {
                    name = typeof(T).Name + " Auto-Generated",
                    hideFlags = HideFlags.DontSave
                };

                _instance = go.AddComponent<T>();
            
                return _instance;
            }
        }

        public virtual void InitializeSingleton()
        {
            if (!Application.isPlaying) return;

            InitializationTime = Time.time;

            var oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);

            if (null != oldInstances)
            {
                if (oldInstances.Any(old => InitializationTime > old.GetComponent<Singleton<T>>().InitializationTime))
                {
                    Destroy(gameObject);
                    return;
                }
            }

            if (_instance != null) return;
            
            _instance = this as T;
        }

        public virtual void ReleaseSingleton()
        {
        }
    }
}
