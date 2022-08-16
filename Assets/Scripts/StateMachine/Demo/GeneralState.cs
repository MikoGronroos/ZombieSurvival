using Finark.AI;
using System.Collections.Generic;
using UnityEngine;

public class GeneralState : State
{

    private Transform _playerTransform;
    private bool _goingRight;

    public GeneralState(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override void EnterState(StateMachine machine)
    {
        Debug.Log("Entered General State!");
    }

    public override void ExitState(StateMachine machine) {   }

    public override void PhysicsRunState(StateMachine machine) { }
     
    public override void RunState(StateMachine machine) 
    {

        if (Input.GetKeyDown(KeyCode.W)) _goingRight = !_goingRight;

        if(_goingRight)
            _playerTransform?.Translate(Vector3.right * 5 * Time.deltaTime);
        else
            _playerTransform?.Translate(-Vector3.right * 5 * Time.deltaTime);

    }
}