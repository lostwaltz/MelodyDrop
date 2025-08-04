using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class EngineParam
    {
    
    }
    
    public class Core : SingletonMono<Core>
    {
        [SerializeField] private SceneData sceneData;
        
        private List<ISingletonMonoInterface> _managers;
        private Dictionary<Type, ISingletonMonoInterface> _managerMap = new();
        
        public static EventHub<EngineEventType, EngineParam> EventContainer { get; private set; } = new ();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInitializeOnLoadMethod()
        {
            Debug.Log("Enter BootEngine");

            SceneManager.LoadScene("Core", LoadSceneMode.Additive);

            EventContainer.Publish(EngineEventType.OnStart, new EngineParam());
        }

        #region INTERFACE

        public static T GetService<T>() where T : SingletonMono<T>
        {
            if(Instance == null)
                return null;
            
            return Instance._managerMap.TryGetValue(typeof(T), out var value) ? value as T : null;
        }

        public static T GetSingleton<T>() where T : Singleton<T>, new()
        {
            return Singleton<T>.Instance;
        }

        #endregion

        private void Awake()
        {
            _managers = gameObject.GetComponentsInChildren<ISingletonMonoInterface>().ToList();
            foreach (var manager in _managers)
                _managerMap[manager.GetSingletonType()] = manager;
            
            InitializeManager();
            
            GetSingleton<ManualLifeCycleManager>().RunAwake();
        }
        
        private async void Start()
        {
            await GetService<SceneChannelManager>().LoadScene(sceneData);
            
            GetSingleton<ManualLifeCycleManager>().RunStart();
        }

        private void OnDestroy()
        {
            GetSingleton<ManualLifeCycleManager>().RunDestroy();

            ReleaseManager();
        }

        private void InitializeManager()
        {
            foreach (var manager in _managers)
                manager.InitializeSingleton();
        }

        private void ReleaseManager()
        {
            foreach (var manager in _managers)
                manager.ReleaseSingleton();
        }
        
    }
}