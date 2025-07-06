public abstract class GameLifeCycleBaseState : BaseState
{
    protected readonly GameLifeCycleManager Handler;

    protected GameLifeCycleBaseState(GameLifeCycleManager handler)
    {
        Handler = handler;
    }
}