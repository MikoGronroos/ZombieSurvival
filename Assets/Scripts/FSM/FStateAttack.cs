using System;

public class FStateAttack : FState
{

    public FStateAttack(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    public override void Enter()
    {
        AnimMethodChannel.EndMeleeAttack += EndMeleeAttackListener;
        _character.AnimationSystem.PlayAnimation(_character.WeaponController.CurrentWeapon.GetWeaponAttackAnimation());
    }

    public override void Exit()
    {
        AnimMethodChannel.EndMeleeAttack -= EndMeleeAttackListener;
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
    }

    private void EndMeleeAttackListener()
    {
        _stateMachine.ChangeState(_character.fStateIdle);
    }


}