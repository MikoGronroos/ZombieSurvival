using Finark.AI;
using UnityEngine;

public class StateInteracting : State
{

    private Animator _animator;

    public StateInteracting(Animator animator)
    {
        _animator = animator;
    }


    public override void EnterState(StateMachine machine)
    {
        _animator.SetBool("looting1", true);
    }

    public override void ExitState(StateMachine machine)
    {
        _animator.SetBool("looting3", true);
        _animator.SetBool("looting2", false);
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
    }


    public void PlaySecondLootAnimation()
    {
        _animator.SetBool("looting2", true);
        _animator.SetBool("looting1", false);
    }

    public void StopLootingAnimation()
    {
        _animator.SetBool("looting3", false);
    }


}
