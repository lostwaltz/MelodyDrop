using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class EngineParam
    {
    
    }
    
    public class Core : MonoBehaviour
    {
        [SerializeField] private SceneData sceneData;
        
        
        [Header("Managers")]
        [SerializeField] private UIManager uiManager;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private SceneChannelManager sceneManager;
        [SerializeField] private DataManager dataManager;
        [SerializeField] private FadeManager fadeManager;
        [SerializeField] private InputManager inputManager;
        
        public static EventHub<EngineEventType, EngineParam> EventContainer { get; private set; } = new ();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInitializeOnLoadMethod()
        {
            Debug.Log("Enter BootEngine");

            SceneManager.LoadScene("Core", LoadSceneMode.Additive);

            EventContainer.Publish(EngineEventType.OnStart, new EngineParam());
        }

        private void Awake()
        {
            InitializeManager();
            
            ManualLifeCycleManager.Instance.RunAwake();
        }
        
        private async void Start()
        {
            await SceneChannelManager.Instance.LoadScene(sceneData);
            
            ManualLifeCycleManager.Instance.RunStart();
        }

        private void OnDestroy()
        {
            ManualLifeCycleManager.Instance.RunDestroy();

            ReleaseManager();
        }

        private void InitializeManager()
        {
            uiManager.InitializeSingleton();
            cameraManager.InitializeSingleton();
            sceneManager.InitializeSingleton();
            dataManager.InitializeSingleton();
            fadeManager.InitializeSingleton();
            inputManager.InitializeSingleton();
        }

        private void ReleaseManager()
        {
            uiManager.ReleaseSingleton();
            cameraManager.ReleaseSingleton();
            sceneManager.ReleaseSingleton();
            dataManager.ReleaseSingleton();
            fadeManager.ReleaseSingleton();
            inputManager.ReleaseSingleton();
        }
        
    }
}