using System;
using Engine;
using UnityEngine;

namespace State.GameLifeCycleBaseState
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private TabEntityGenerator generator;

        public ServiceContainer ServiceContainer => generator.ServiceContainer;

        private readonly StateMachine _stateMachine = new StateMachine();

        public void Start()
        {
            PatternSelectState patternSelectState = new(this);
            EntitySpawnState spawnState = new(this);
            FeverState feverState = new(this);
            CooldownState cooldownState = new(this);
            GameOverState gameOverState = new(this);

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
}