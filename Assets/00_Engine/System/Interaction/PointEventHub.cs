using Engine;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PointEventHub : MonoBehaviour
{
    private readonly Dictionary<PointEventType, Dictionary<GameObject, PointEventBase>> _eventMap = new();

    public void Register(PointEventType type, PointEventBase evt)
    {
        if (_eventMap.TryGetValue(type, out var dict) == false)
            _eventMap[type] = dict = new Dictionary<GameObject, PointEventBase>();

        dict[evt.gameObject] = evt;
    }

    public void Bind(GameObject go, PointEventType type, Action<PointerEventData> callback)
    {
        if (_eventMap.TryGetValue(type, out var dict) && dict.TryGetValue(go, out var evt))
            evt.OnPointEvent += callback;
        else
            Log.Warn($"[PointEventContainer] {type} can't find in {go.name}");
    }
}
