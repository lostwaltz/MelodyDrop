using Engine;
using UnityEngine;

public class CameraBinder : ManualMonoBehaviour
{
    [SerializeField] private Camera cam;
    
    private void Reset()
    {
        cam = GetComponentInChildren<Camera>();
    }
    
    public override void ManualAwake()
    {
        Core.GetService<CameraManager>().ChangeMainCamera(cam);
    }
}
