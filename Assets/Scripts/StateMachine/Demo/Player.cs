using Finark.AI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{

    [SerializeField] private Transform playerTransform;

    private void Awake()
    {

        GeneralState generalState = new GeneralState(playerTransform);
        IdleState idleState = new IdleState();
        TeleportState teleportState = new TeleportState(playerTransform);

        AddTransition(idleState, generalState, new List<Func<bool>> { IsFalling });
        AddAnyTransition(teleportState, new List<Func<bool>> { IsMoreOnRight });
        AddTransition(teleportState, generalState, new List<Func<bool>> { PressedSButton });

        SwitchState(idleState);
    }

    public bool IsFalling()
    {
        return true;
    }

    public bool IsMoreOnRight()
    {
        return transform.position.x > 5;
    }

    public bool PressedSButton()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
    
}