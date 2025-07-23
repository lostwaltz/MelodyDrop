using UnityEngine;
using UnityEngine.EventSystems;
using Engine;
using Unity.VisualScripting;

public class TestObject : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PointEventHub>().Bind(gameObject, PointEventType.Drag, TestFunc);
    }

    private void TestFunc(PointerEventData eventData)
    {
        Vector3 screenPos = eventData.position;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f));

        transform.position = worldPos;
    }
}
