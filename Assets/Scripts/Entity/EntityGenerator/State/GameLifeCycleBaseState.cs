
namespace State.GameLifeCycleBaseState
{
    public abstract class GameLifeCycleBaseState : BaseState
    {
        protected readonly GameManager Handler;

        protected GameLifeCycleBaseState(GameManager handler)
        {
            Handler = handler;
        }
    }
}