using UnityEngine;

public class FState
{

    protected FStateMachine _stateMachine;
    protected InputEventChannel _inputEventChannel;
    protected FCharacter _character;

    public FState(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel)
    {
        _character = character;
        _stateMachine = stateMachine;
        _inputEventChannel = inputEventChannel;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
