using UnityEngine;

public class FStateReload : FState
{
    public FStateReload(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {
    }

    public override void Enter()
    {
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
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
    }

}