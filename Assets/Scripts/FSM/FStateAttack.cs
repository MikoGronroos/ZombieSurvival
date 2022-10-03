using System;

public class FStateAttack : FState
{
    public FStateAttack(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    public override void Enter()
    {
        AnimMethodChannel.ResetAttackEvent += ResetAttack;
    }

    public override void Exit()
    {
        AnimMethodChannel.ResetAttackEvent += ResetAttack;
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        _character.AnimationSystem.PlayAnimation(_character.WeaponController.CurrentWeapon.GetWeaponAttackAnimation());
    }

    private void ResetAttack()
    {
        _stateMachine.ChangeState(_character.fStateIdle);
    }

}