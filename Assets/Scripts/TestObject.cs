using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Engine;
using Unity.VisualScripting;

public class TestObject : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(transform.position);

    }

    private void Update()
    {
        Debug.Log(transform.position);
    }
}
