using Finark.AI;
using UnityEngine;

public class StateMoving : State
{


    private Animator _animator;
    private CharacterController _controller;
    private Transform _transform;

    private Vector3 _direction;

    private float _turnSmoothTime = 0.09f;

    private float _turnSmoothVelocity;

    public StateMoving(Animator animator, CharacterController controller, Transform transform)
    {
        _animator = animator;
        _controller = controller;
        _transform = transform;
    }

    public override void EnterState(StateMachine machine)
    {
        _animator.SetBool("isWalking", true);
    }

    public override void ExitState(StateMachine machine)
    {
        _animator.SetBool("isWalking", false);
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {

        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            _transform.rotation = Quaternion.Euler(0, angle, 0);
        }

    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = _animator.deltaPosition;

        _controller.Move(velocity);
    }
}
