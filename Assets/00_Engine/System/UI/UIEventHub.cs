using Engine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class UIEventHub
{
    private readonly Dictionary<PointEventType, Dictionary<GameObject, UIPointEventBase>> _eventMap = new();

    public void Register(PointEventType type, UIPointEventBase evt)
    {
        if (_eventMap.TryGetValue(type, out var dict) == false)
            _eventMap[type] = dict = new Dictionary<GameObject, UIPointEventBase>();

        dict[evt.gameObject] = evt;
    }

    public void Bind(GameObject go, PointEventType type, Action<PointerEventData> callback)
    {
        if (_eventMap.TryGetValue(type, out var dict) && dict.TryGetValue(go, out var evt))
            evt.OnPointEvent += callback;
        else
            Log.Warn($"[UIEventContainer] {type} can't find in {go.name}");
    }
}