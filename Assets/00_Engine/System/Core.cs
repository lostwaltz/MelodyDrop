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
            
            ShortCut.Get<ManualLifeCycleManager>().RunAwake();
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

        private async void Start()
        {
            await ShortCut.Get<SceneChannelManager>().LoadScene(sceneData);
            
            ManualLifeCycleManager.Instance.RunStart();
        }
    }
}