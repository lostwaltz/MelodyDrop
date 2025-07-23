using Engine;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PointEventBase : ManualMonoBehaviour
{
    [FormerlySerializedAs("_root")] [SerializeField] protected PointEventHub root;
    public override void ManualAwake()
    {
        if (root == null)
        {
            Transform transformRoot = transform.root;
            this.root = transformRoot.GetComponent<PointEventHub>() ?? transformRoot.GetComponentInChildren<PointEventHub>();
            if (this.root == null)
            {
                Log.Warn($"{name} can't find PointEventContainer.");
                return;
            }
        }

        root.Register(PointEventType.Click, this);
    }

    protected void Reset()
    {
        Transform transformRoot = gameObject.transform;
        while (transformRoot.parent != null)
        {
            transformRoot = transformRoot.parent;
        }

        root = transformRoot.GetComponent<PointEventHub>();
        root ??= transformRoot.GetComponentInChildren<PointEventHub>();

        if (root == null)
            Log.Warn($"{name} can't find PointEventContainer.");
    }

    public Action<PointerEventData> OnPointEvent { get; set; }

}
