using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engine
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private Camera mainCamera;

        public void ChangeMainCamera(Camera cam)
        {
            Destroy(mainCamera.gameObject);
            
            mainCamera = cam;
            mainCamera.transform.SetParent(transform);
        }

        public Camera GetMainCamera()
        {
            return mainCamera;
        }
        
        public Vector3 GetBottomWorldPosition(float normalizedX, float distanceFromCamera, Camera cam = null)
        {
            if (cam == null) cam = Camera.main;

            return mainCamera.ViewportToWorldPoint(new Vector3(normalizedX, 0f, distanceFromCamera));
        }
    }
}