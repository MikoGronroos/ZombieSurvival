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

        AddAnyTransition(stateIdle, new List<Func<bool>> { IsIdle, stateAttacking.IsNotAttacking, IsNotInteracting });
        AddAnyTransition(stateMoving, new List<Func<bool>> { IsMoving, stateAttacking.IsNotAttacking, IsNotInteracting });
        AddAnyTransition(stateAttacking, new List<Func<bool>> { IsAttacking, IsNotInteracting });
        AddAnyTransition(stateInteracting, new List<Func<bool>> { IsInteracting, stateAttacking.IsNotAttacking });

        #endregion

        SwitchState(stateIdle);
    }

    #region Conditions

    private bool IsMoving()
    {
        return InputSystem.Instance.MoveVector.magnitude != 0;
    }

    private bool IsIdle()
    {
        return InputSystem.Instance.MoveVector.magnitude == 0 && !InputSystem.Instance.IsInteracting && !InputSystem.Instance.IsAttacking;
    }

    private bool IsAttacking()
    {
        return InputSystem.Instance.IsAttacking;
    }

    private bool IsInteracting()
    {
        return (bool)interactionData.IsInteractingEvent?.Invoke();
    }

    private bool IsNotInteracting()
    {
        return !(bool)interactionData.IsInteractingEvent?.Invoke();
    }

    #endregion

    #region Animation methods

    public void ResetAttack()
    {
        stateAttacking.ResetAttack();
    }

    public void PlaySecondLootAnimation()
    {
        stateInteracting.PlaySecondLootAnimation();
    }

    public void StopLootingAnimation()
    {
        stateInteracting.StopLootingAnimation();
    }

    #endregion

}
