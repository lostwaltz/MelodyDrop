
using System;

namespace Engine
{
    public interface IEvent
    {
    }

    public struct InitEvent : IEvent
    {
    }

    public struct SceneProgressEvent : IEvent
    {
        public float Value;
    }
}