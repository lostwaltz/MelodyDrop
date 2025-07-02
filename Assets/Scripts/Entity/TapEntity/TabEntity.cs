using Engine;
using UnityEngine;

public class TabEntity : ManualMonoBehaviour
{
    private TabEntityData _data;

    [SerializeField] private EntityComponentController controller;
    
    public override void ManualStart()
    {
        base.ManualStart();

        Vector3 position = CameraManager.Instance.GetBottomWorldPosition(0.5f,
            Mathf.Abs(gameObject.transform.position.z - CameraManager.Instance.GetMainCamera().transform.position.z),
            CameraManager.Instance.GetMainCamera());

        transform.position = position;
        
        controller.RunOnPlay();
    }
}
