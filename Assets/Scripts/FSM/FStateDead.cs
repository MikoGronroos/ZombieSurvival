public class FStateDead : FState
{
    public FStateDead(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    public override void Enter()
    {
        _character.AnimationSystem.PlayAnimation("Death");
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
    }

}