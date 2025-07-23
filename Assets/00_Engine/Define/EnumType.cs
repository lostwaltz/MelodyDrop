using System;

namespace Engine
{
    public enum EngineEventType
    {
        OnStart, OnGameStart, OnEnd, OnPause, OnResume, OnStop
    }
    
    public enum SceneType { ActiveScene, UserInterface, HUD, Cinematic, Environment, Loading, Core }
    
    [Flags]
    public enum InteractionType
    {
        None        = 0,
        Cube        = 1 << 0,
        GlowPanel   = 1 << 1,
        All         = ~0
    }
}
