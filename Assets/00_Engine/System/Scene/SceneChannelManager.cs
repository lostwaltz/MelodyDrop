using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


namespace Engine
{
    public class SceneChannelManager : SingletonMono<SceneChannelManager>
    {
        [SerializeField] public SceneContainer sceneContainer;

        [ItemCanBeNull] public readonly Dictionary<SceneType, SceneData> SceneDictionary = new();
        
        private readonly Dictionary<SceneType, AsyncOperationHandle<SceneInstance>> _handle = new();
        
        public override void InitializeSingleton()
        {
            base.InitializeSingleton();
            
            // SceneDictionary[SceneType.ActiveScene] =
            //     sceneContainer.SceneDictionary[BootEngine.StartScene];
        }

        public async UniTask LoadScene(SceneData sceneData, bool unLoadScene = true)
        {
            SceneType channel = sceneData.sceneType;
            if(sceneData.reference == null) return;

            if (SceneDictionary.TryGetValue(channel, out _))
            {
                if(unLoadScene)
                    await UnloadScene(channel);   
            }
            
            if(SceneDictionary.TryGetValue(channel, out _))
                SceneDictionary[channel] = sceneData;
            else SceneDictionary.Add(channel, sceneData);

            LoadingProgress progress = new LoadingProgress();
            progress.ProgressChanged += (value) => EventBus<SceneProgressEvent>.Raise(new SceneProgressEvent() { Value = value });

            switch (sceneData.reference.State)
            {
                case SceneReferenceState.Regular:
                    var handle = SceneManager.LoadSceneAsync(SceneDictionary[channel]?.Name, LoadSceneMode.Additive);
                    while (handle is { isDone: false })
                    {
                        progress.Report(handle.progress);
                        await UniTask.Delay(100);
                    }
                    break;
                case SceneReferenceState.Addressable:
                    var task = Addressables.LoadSceneAsync(sceneData.reference.Path, LoadSceneMode.Additive);
                    
                    if(_handle.TryGetValue(sceneData.sceneType, out _))
                        _handle[sceneData.sceneType] = task;
                    else _handle.Add(sceneData.sceneType, task);
                    
                    while (_handle[sceneData.sceneType].IsDone == false)
                    {
                        progress.Report(_handle[sceneData.sceneType].PercentComplete);
                        await UniTask.Delay(100);
                    }
                    break;
                case SceneReferenceState.Unsafe:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await UniTask.Yield();
            
            if (sceneData.sceneType == SceneType.ActiveScene)
                SceneManager.SetActiveScene(sceneData.reference.LoadedScene);
            
        }

        public async UniTask UnloadScene(SceneType sceneType)
        {
            if (!SceneDictionary.TryGetValue(sceneType, out SceneData data)) return;
            if(data?.reference == null) return;
            
            switch (data.reference.State)
            {
                case SceneReferenceState.Regular:
                    await SceneManager.UnloadSceneAsync(data.Name);
                    break;
                case SceneReferenceState.Addressable:
                     await Addressables.UnloadSceneAsync(_handle[sceneType]);
                     _handle.Remove(sceneType);
                     break;
                case SceneReferenceState.Unsafe:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SceneDictionary.Remove(sceneType);
        }
    }
}