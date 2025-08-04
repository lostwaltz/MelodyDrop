using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class TimeManager : SingletonMono<TimeManager>
    {
        private readonly Dictionary<TimeType, TimeSource> _sources = new();
        
        public override void InitializeSingleton()
        {
            base.InitializeSingleton();

            TimeSource[] sources = GetComponentsInChildren<TimeSource>();

            foreach (var source in sources)
                _sources[source.Data.timeType] = source;
        }
        
        private void Update()
        {
            foreach (var src in _sources.Values)
                src.UpdateTimeSource();
        }

        public float GetDelta(TimeType type) => _sources[type].DeltaTime;
        public float GetElapsed(TimeType type) => _sources[type].ElapsedTime;

        public TimeSource GetSource(TimeType type) => _sources[type];
    }
}