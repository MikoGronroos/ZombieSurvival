using Finark.AI;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{

    private Transform _playerTransform;

    public TeleportState(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override void EnterState(StateMachine machine)
    {
        Debug.Log("I Entered Teleport State");
    }

    public override void ExitState(StateMachine machine) {   }

    public override void PhysicsRunState(StateMachine machine) { }

    public override void RunState(StateMachine machine)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerTransform.position = new Vector3(0,5,0);
        }
    }
}