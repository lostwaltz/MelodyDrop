using System.Collections.Generic;
using DG.Tweening;
using Engine;
using UnityEngine;

public class TabEntitySpawner : MonoBehaviour
{
    public bool CanSpawn { get; set; } = true;

    public void SpawnEntity(EntityData entityData, ColorData colorData)
    {
        Vector3 worldPos;

        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Core.GetService<CameraManager>().GetMainCamera(), transform.position);

        screenPoint.z = 5f;

        worldPos = Core.GetService<CameraManager>().GetMainCamera().ScreenToWorldPoint(screenPoint);

        // 3. 3D 오브젝트 스폰
        Instantiate(Resources.Load<GameObject>("Prefabs/Entity/TabEntity"), worldPos, Quaternion.identity);
        
        //Instantiate(Resources.Load<GameObject>("Prefabs/Entity/TabEntity"), transform.position, Quaternion.identity);
    }
}
