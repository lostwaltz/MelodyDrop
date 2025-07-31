using System;
using DG.Tweening;
using UnityEngine;

public class BaseMove : EntityComponent
{
    private float _speed = 1f;
    
    private void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime), Space.World);
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        GetRoot<TabEntity>().EventHub.Publish(TabEntity.EntityEvent.EntityDestroy);
    }
}
