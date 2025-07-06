using System;
using Engine;
using UnityEngine;

public class GameLifeCycleManager : Singleton<GameLifeCycleManager>
{
    [SerializeField] private TabEntityGenerator generator;
    
    public ServiceContainer ServiceContainer => generator.ServiceContainer;
    
    private readonly StateMachine _stateMachine = new StateMachine();

    public void Start()
    {
        GameLifeCyclePatternSelectState patternSelectState = new (this);
        GameLifeCycleEntitySpawnState spawnState = new (this);
        GameLifeCycleFeverState feverState = new(this);
        GameLifeCycleCooldownState cooldownState = new (this);
        GameLifeCycleGameOverState gameOverState = new(this);

        ServiceContainer.Register(spawnState);
        
        _stateMachine.AddTransition(patternSelectState, spawnState, new FuncPredicate(() => true));
        
        _stateMachine.SetState(patternSelectState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Generate(SpawnPatternData data)
    {
        generator.Generate(data);
    }
}