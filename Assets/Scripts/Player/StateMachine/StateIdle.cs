using Finark.AI;
using UnityEngine;

public class StateIdle : State
{


    private Animator _animator;

    public StateIdle(Animator animator)
    {
        _animator = animator;
    }

    public override void EnterState(StateMachine machine)
    {
        _animator.SetBool("isIdle", true);
    }

    public override void ExitState(StateMachine machine)
    {
        _animator.SetBool("isIdle", false);
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
    }
}