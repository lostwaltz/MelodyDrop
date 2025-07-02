using Engine;
using UnityEngine;

public class UIBackground : UIBase
{
    [SerializeField] private Canvas canvas;
    
    public override void ManualStart()
    {
        base.ManualStart();

        canvas.worldCamera = CameraManager.Instance.GetMainCamera();
    }
}
