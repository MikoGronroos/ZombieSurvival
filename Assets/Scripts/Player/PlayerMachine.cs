using Finark.AI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : StateMachine
{

    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    [SerializeField] private InteractionData interactionData;

    StateMoving stateMoving;
    StateInteracting stateInteracting;
    StateAttacking stateAttacking;
    StateIdle stateIdle;

    private void Awake()
    {
        stateMoving = new StateMoving(animator, controller, transform);
        stateInteracting = new StateInteracting(animator);
        stateAttacking = new StateAttacking(animator);
        stateIdle = new StateIdle(animator);

        #region Transitions

        AddAnyTransition(stateIdle, new List<Func<bool>> { IsIdle, stateAttacking.IsNotAttacking });
        AddAnyTransition(stateMoving, new List<Func<bool>> { IsMoving, stateAttacking.IsNotAttacking });
        AddAnyTransition(stateAttacking, new List<Func<bool>> { IsAttacking });
        //AddAnyTransition(stateInteracting, new List<Func<bool>> { stateAttacking.IsNotAttacking });

        #endregion

        SwitchState(stateIdle);
    }

    #region Conditions

    public bool IsMoving()
    {
        return InputSystem.Instance.MoveVector.magnitude != 0;
    }

    public bool IsIdle()
    {
        return InputSystem.Instance.MoveVector.magnitude == 0 && !InputSystem.Instance.IsInteracting && !InputSystem.Instance.IsAttacking;
    }

    public bool IsAttacking()
    {
        return InputSystem.Instance.IsAttacking;
    }

    #endregion

    #region Animation methods

    public void ResetAttack()
    {
        stateAttacking.ResetAttack();
    }

    public void PickupIdleAnimation()
    {
        stateInteracting.CanPlayMiddleAnim();
    }

    #endregion

}
