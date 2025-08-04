using System;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public enum GameEventType
{
    SpawnEntity
}

public class SpawnData
{
    public SpawnPatternData Data;
    public Action OnGenerateComplete;
}

public class GameManager : SingletonMono<GameManager>
{
    private readonly StateMachine _stateMachine = new();
    
    public Action<SpawnData> OnSpawn;
    
    private readonly Dictionary<Type, GameCycleBaseState> _gameCycleStates = new();
    
    private void Awake()
    {
        InitializeSingleton();

        ReadyState readyState = new ReadyState();
        GenerateState generateState = new GenerateState();
        DelayState delayState = new DelayState();
        GameOverState gameOverState = new GameOverState();
        
        _gameCycleStates[typeof(ReadyState)] = readyState;
        _gameCycleStates[typeof(GenerateState)] = generateState;
        _gameCycleStates[typeof(DelayState)] = delayState;
        _gameCycleStates[typeof(GameOverState)] = gameOverState;
        
        _stateMachine.AddTransition(readyState, generateState, readyState.IsReady());
        _stateMachine.AddTransition(generateState, delayState, generateState.IsDoneGenerating());
        _stateMachine.AddTransition(delayState, generateState, new FuncPredicate(() => false));
        
        _stateMachine.AddAnyTransition(gameOverState, new FuncPredicate(() => false));
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _stateMachine.SetState(_gameCycleStates[typeof(ReadyState)]);
    }

    private void Update()
    {
        _stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
