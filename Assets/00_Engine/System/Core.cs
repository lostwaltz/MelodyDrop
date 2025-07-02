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
            // Init System
            //await InitEngine();
            
            ManualLifeCycleManager.Instance.RunAwake();
        }

        private async void Start()
        {
            await SceneChannelManager.Instance.LoadScene(sceneData);
            
            ManualLifeCycleManager.Instance.RunStart();
        }
    }
}