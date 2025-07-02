
using System;

namespace Engine
{
    public interface IEvent
    {
    }

    public struct PlayerEvent : IEvent
    {
    }

    public struct SceneProgressEvent : IEvent
    {
        public float Value;
    }
}