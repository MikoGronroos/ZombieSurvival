using Finark.AI;
using UnityEngine;

public class IdleState : State
{

    public IdleState()
    {

    }

    public override void EnterState(StateMachine machine) 
    {
        Debug.Log("I entered a state.");
    }

    public override void ExitState(StateMachine machine)
    {
        Debug.Log("I exited a state.");
    }

    public override void PhysicsRunState(StateMachine machine) { }

    public override void RunState(StateMachine machine)
    {
        Debug.Log("State running.");
    }
}