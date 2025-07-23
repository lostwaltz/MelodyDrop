using UnityEngine;

public class TabEntityLifeHandler : EntityComponent
{
    [SerializeField] private float destroyPointY;

    public override void Init()
    {
        GetComponent<TabEntity>().EventHub.Subscribe(TabEntity.EntityEvent.EntityDestroy, DestroyEntity);
    }

    private void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
