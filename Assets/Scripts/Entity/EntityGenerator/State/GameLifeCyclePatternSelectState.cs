using Engine;
using UnityEngine;

public class GameLifeCyclePatternSelectState : GameLifeCycleBaseState
{
    public GameLifeCyclePatternSelectState(GameLifeCycleManager handler) : base(handler)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Handler.ServiceContainer.Get<GameLifeCycleEntitySpawnState>()
            .SetSpawnPattern(ShortCut.Get<DataManager>().SpawnPatternDataMap[Random.Range(100000, 100002)]);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}