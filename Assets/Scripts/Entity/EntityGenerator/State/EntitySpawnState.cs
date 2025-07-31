using System.Collections;
using UnityEngine;

namespace State.GameLifeCycleBaseState
{
    public class EntitySpawnState : GameLifeCycleBaseState
    {
        private SpawnPatternData _data;

        public EntitySpawnState(GameManager handler) : base(handler)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Handler.Generate(_data);
        }

        public void SetSpawnPattern(SpawnPatternData data)
        {
            _data = data;
        }
    }
}