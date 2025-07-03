using System;
using DG.Tweening;
using UnityEngine;

public class BaseMove : EntityComponent
{
    private void Start()
    {
        Vector3 pos = transform.position;

        transform.DOMove(new Vector3(pos.x, 0.6f, pos.z), 5f).SetEase(Ease.Linear).OnComplete(CallDoneMove);
    }

    private void CallDoneMove()
    {
        GetComponent<TabEntity>().EventHub.Publish(TabEntity.EntityEvent.EntityDestroy);
    }
}
