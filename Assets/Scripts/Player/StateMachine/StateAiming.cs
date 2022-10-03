using Finark.AI;
using UnityEngine;

public class StateAiming : State
{

    private Animator _animator;
    private Transform _transform;

    public StateAiming(Animator animator, Transform transform)
    {
        _animator = animator;
        _transform = transform;
    }

    public override void EnterState(StateMachine machine)
    {
        _animator.SetBool("isAiming", true);
    }

    public override void ExitState(StateMachine machine)
    {
        _animator.SetBool("isAiming", false);
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            _transform.LookAt(new Vector3(pointToLook.x, _transform.position.y, pointToLook.z));
        }

    }
}