using System;

public class FStateAttack : FState
{
    public FStateAttack(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    public override void Enter()
    {
        AnimMethodChannel.ResetAttackEvent += ResetAttack;
        if ((_character.WeaponController.CurrentWeapon as RangedWeapon).CanShoot())
        {
            _character.AnimationSystem.PlayAnimation(_character.WeaponController.CurrentWeapon.GetWeaponAttackAnimation());
        }
        else
        {
            _stateMachine.ChangeState(_character.fStateIdle);
        }
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
    }

    private void ResetAttack()
    {
        _stateMachine.ChangeState(_character.fStateIdle);
    }

}