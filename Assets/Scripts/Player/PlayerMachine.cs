using Finark.AI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InputEventChannel inputEventChannel;

    StateMoving stateMoving;
    StateInteracting stateInteracting;
    StateAttacking stateAttacking;
    StateIdle stateIdle;
    StateAiming stateAiming;

    private void Awake()
    {
        stateMoving = new StateMoving(animator, controller, transform);
        stateInteracting = new StateInteracting(animator);
        stateAttacking = new StateAttacking(animator);
        stateIdle = new StateIdle(animator);
        stateAiming = new StateAiming(animator, transform);
    }
}
