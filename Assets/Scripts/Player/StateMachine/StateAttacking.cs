using Finark.AI;
using UnityEngine;

public class StateAttacking : State
{

    private Animator _animator;

    private bool _canAttack;
    private bool _isAttacking;

    public StateAttacking(Animator animator)
    {
        _animator = animator;
    }

    public override void EnterState(StateMachine machine)
    {
        _canAttack = true;

        if (_canAttack)
        {
            _canAttack = false;

            _isAttacking = true;

            _animator.SetBool("isAttacking", true);
        }

    }

    public override void ExitState(StateMachine machine)
    {
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
    }

    public void ResetAttack()
    {
        _animator.SetBool("isAttacking", false);
        _isAttacking = false;
        _canAttack = true;
    }

    public bool IsNotAttacking()
    {
        return !_isAttacking;
    }

}
