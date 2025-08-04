using UnityEngine;

namespace Engine
{
    public class CustomWaitForSec : CustomYieldInstruction
    {
        private readonly TimeType _timeType;
        private readonly float _duration;
        private float _elapsed = 0;

        public CustomWaitForSec(float seconds, TimeType timeType)
        {
            _duration = seconds;
            _timeType = timeType;
        }

        public override bool keepWaiting
        {
            get
            {
                _elapsed += Core.GetService<TimeManager>().GetDelta(_timeType);
                return _elapsed < _duration;
            }
        }
    }
}