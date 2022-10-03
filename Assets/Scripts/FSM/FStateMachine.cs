public class FStateMachine
{
    public FState currentState;

    public void Initialize(FState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(FState newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }


}
