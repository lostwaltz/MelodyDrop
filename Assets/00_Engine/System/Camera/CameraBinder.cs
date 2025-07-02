using Engine;
using UnityEngine;

public class CameraBinder : ManualMonoBehaviour
{
    [SerializeField] private Camera cam;
    
    private void Reset()
    {
        // 에디터에서 컴포넌트가 붙자마자 자동 세팅
        cam = GetComponentInChildren<Camera>();
    }
    
    public override void ManualAwake()
    {
        CameraManager.Instance.ChangeMainCamera(cam);
    }
}
