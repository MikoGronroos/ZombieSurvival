public class FStateIdle : FState
{
    public FStateIdle(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    private bool _walking;
    private bool _running;
    private bool _aiming;

    public override void Enter()
    {
        _walking = false;
        _running = false;
        _aiming = false;
    }

    public override void HandleInput()
    {
        if (_inputEventChannel.MoveVector.magnitude != 0)
        {
            _walking = true;
        }

        if (_inputEventChannel.IsRunning && _inputEventChannel.MoveVector.magnitude != 0)
        {
            _running = true;
        }

        if (_inputEventChannel.IsAiming)
        {
            _aiming = true;
        }
    }

    public override void LogicUpdate()
    {

        if (_aiming)
        {
            _stateMachine.ChangeState(_character.fStateAim);
        }

        if (_walking)
        {
            _stateMachine.ChangeState(_character.fStateWalk);
        }

        if (_running)
        {
            _stateMachine.ChangeState(_character.fStateRun);
        }

        _character.AnimationSystem.PlayAnimation("Idle");
    }

}
