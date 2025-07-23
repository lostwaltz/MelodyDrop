using System;
using DG.Tweening;
using UnityEngine;

public class BaseMove : EntityComponent
{
    private void Start()
    {
        Vector3 pos = transform.position;

        transform.DOMoveY(0.5f, 8f).SetEase(Ease.Linear).OnComplete(CallDoneMove);
    }

    private void CallDoneMove()
    {
        GetComponent<TabEntity>().EventHub.Publish(TabEntity.EntityEvent.EntityDestroy);
    }
}
