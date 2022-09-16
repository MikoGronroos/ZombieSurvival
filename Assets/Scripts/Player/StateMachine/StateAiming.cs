using Finark.AI;
using UnityEngine;

public class StateAiming : State
{

    private Animator _animator;

    public StateAiming(Animator animator)
    {
        _animator = animator;
    }

    public override void EnterState(StateMachine machine)
    {
        _animator.SetBool("isAiming", true);
    }

    public override void ExitState(StateMachine machine)
    {
        _animator.SetBool("isAiming", false);
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
    }
}