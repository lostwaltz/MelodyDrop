using System.Collections;
using UnityEngine;

public class GameLifeCycleEntitySpawnState : GameLifeCycleBaseState
{
    private SpawnPatternData _data;
    
    public GameLifeCycleEntitySpawnState(GameLifeCycleManager handler) : base(handler)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Handler.Generate(_data);
    }

    public void SetSpawnPattern(SpawnPatternData data)
    {
        _data = data;
    }
}