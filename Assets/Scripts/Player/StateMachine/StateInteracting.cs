using Finark.AI;
using UnityEngine;

public class StateInteracting : State
{

    private Animator _animator;

    private bool _canPlayMiddleAnim;

    public StateInteracting(Animator animator)
    {
        _animator = animator;
    }


    public override void EnterState(StateMachine machine)
    {
        _animator.SetBool("looting1", true);
        _animator.SetBool("looting2", false);
        _animator.SetBool("looting3", false);
    }

    public override void ExitState(StateMachine machine)
    {
        _canPlayMiddleAnim = false;
        _animator.SetBool("looting3", true);
        _animator.SetBool("looting2", false);
        _animator.SetBool("looting1", false);
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
        if (_canPlayMiddleAnim)
        {
            _animator.SetBool("looting2", true);
            _animator.SetBool("looting3", false);
            _animator.SetBool("looting1", false);
        }
    }

    public void CanPlayMiddleAnim()
    {
        _canPlayMiddleAnim = true;
    }

}
