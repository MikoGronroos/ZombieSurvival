using UnityEngine;

public class FStateReload : FState
{
    public FStateReload(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    public override void Enter()
    {
        AnimMethodChannel.ResetReloadEvent += ResetReload;
        if ((_character.WeaponController.CurrentWeapon as RangedWeapon).CanReload())
        {
            _character.AnimationSystem.PlayAnimation(_character.WeaponController.CurrentWeapon.GetWeaponReloadAnimation());
        }
        else
        {
            Debug.Log("Can't Reload");
            _stateMachine.ChangeState(_character.fStateIdle);
        }
    }

    public override void Exit()
    {
        AnimMethodChannel.ResetReloadEvent -= ResetReload;
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
    }

    private void ResetReload()
    {
        _stateMachine.ChangeState(_character.fStateIdle);
    }

}