using Engine;
using UnityEngine;

public class GenerateState : GameCycleBaseState
{
    private bool _isDoneGenerating = false;
    
    public SpawnPatternData SpawnPatternData;

    public override void OnEnter()
    {
        base.OnEnter();

        Generate();
    }

    private void Generate()
    {
        SpawnPatternData = Core.GetService<DataManager>().SpawnPatternDataMap[100000];
        
        GameManager.Instance.OnSpawn?.Invoke(new SpawnData { Data = SpawnPatternData, OnGenerateComplete = DoneGenerating});
    }

    private void DoneGenerating()
    {
        _isDoneGenerating = true;
    }
    
    public FuncPredicate IsDoneGenerating()
    {
        return new FuncPredicate(() => _isDoneGenerating);
    }
}
