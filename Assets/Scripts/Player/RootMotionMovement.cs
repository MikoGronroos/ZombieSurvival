using UnityEngine;

public class RootMotionMovement : MonoBehaviour
{

    [SerializeField] private Vector3 direction;

    [SerializeField] private float turnSmoothTime;

    private float _turnSmoothVelocity;

    private CharacterController _controller;
    private Animator _playerAnimator;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        AnimationController();
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = _playerAnimator.deltaPosition;

        _controller.Move(velocity);
    }

    private void AnimationController()
    {

        if (direction.x == 0 && direction.z == 0)
        {
            _playerAnimator.Play("Relax-Idle");
        }
        else
        {
            _playerAnimator.Play("Unarmed-Run-Forward");
        }

    }
}
