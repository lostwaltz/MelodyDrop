using Engine;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PointEventHub : MonoBehaviour
{
    private readonly Dictionary<PointEventType, Dictionary<GameObject, PointEventBase>> _eventMap = new();
    private readonly List<(GameObject go, PointEventType type, Action<PointerEventData> cb)> _pendingBinds = new();

    public void Register(PointEventType type, PointEventBase evt)
    {
        if (_eventMap.TryGetValue(type, out var dict) == false)
            _eventMap[type] = dict = new Dictionary<GameObject, PointEventBase>();

        dict[evt.gameObject] = evt;

        for (int i = _pendingBinds.Count - 1; i >= 0; i--)
        {
            var (go, eventType, action) = _pendingBinds[i];
            
            if (go != evt.gameObject || eventType != type) continue;
            
            evt.OnPointEvent += action;
            _pendingBinds.RemoveAt(i);
        }
    }
    
    public void Unregister(PointEventType type, PointEventBase evt)
    {
        if (!_eventMap.TryGetValue(type, out var dict)) return;

        if (!dict.ContainsKey(evt.gameObject)) return;
        
        dict.Remove(evt.gameObject);

        if (dict.Count == 0)
            _eventMap.Remove(type);
    }

    public void Bind(GameObject go, PointEventType type, Action<PointerEventData> callback)
    {
        if (_eventMap.TryGetValue(type, out var dict) && dict.TryGetValue(go, out var evt))
        {
            evt.OnPointEvent += callback;
            return;
        }

        _pendingBinds.Add((go, type, callback));
    }
}
