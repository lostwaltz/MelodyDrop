using Engine;
using UnityEngine;

namespace State.GameLifeCycleBaseState
{
    public class PatternSelectState : GameLifeCycleBaseState
    {
        public PatternSelectState(GameManager handler) : base(handler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Handler.ServiceContainer.Get<EntitySpawnState>()
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
}