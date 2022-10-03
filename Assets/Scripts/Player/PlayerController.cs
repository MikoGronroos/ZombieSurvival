using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float walkMovemenSpeed;
    [SerializeField] private float runMovemenSpeed;
    [SerializeField] private float aimMovementSpeed;

    private float _currentMovementSpeed
    {
        get
        {
            if (inputEventChannel.IsRunning)
            {
                return runMovemenSpeed;
            }
            if (inputEventChannel.IsAiming)
            {
                return aimMovementSpeed;
            }
            return walkMovemenSpeed;
        }
    }

    #region Rotation

    [Header("Rotation")]

    [SerializeField] private float turnSmoothTime = 0.04f;

    private Vector3 _direction;

    private float _turnSmoothVelocity;

    #endregion


    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InputEventChannel inputEventChannel;

    [SerializeField] private PlayerState currentPlayerState;

    private AnimationSystem _animationSystem = new AnimationSystem();
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _animationSystem.SetupAnimationSystem(animator);
    }

    private void RotateMovingPlayer()
    {

        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private bool IsMoving()
    {
        return inputEventChannel.MoveVector.magnitude != 0;
    }

    private void MoveOnlyForward()
    {
        controller.Move(transform.forward * _currentMovementSpeed * Time.deltaTime);
    }

    private void MoveTowardsInput()
    {
        Vector3 moveDir = inputEventChannel.MoveVector.normalized;
        controller.Move(moveDir * _currentMovementSpeed * Time.deltaTime);
    }

    private void RotateTowardsMousePosition()
    {
        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void Update()
    {

        switch (currentPlayerState)
        {
            case PlayerState.Idle:
                if (inputEventChannel.IsAiming)
                {
                    currentPlayerState = PlayerState.Aiming;
                    break;
                }
                if (IsMoving())
                {
                    currentPlayerState = inputEventChannel.IsRunning ? PlayerState.Running : PlayerState.Walking;
                    break;
                }
                break;
            case PlayerState.Walking:
                if (inputEventChannel.IsAiming)
                {
                    currentPlayerState = PlayerState.Aiming;
                    break;
                }
                if (!IsMoving())
                {
                    currentPlayerState = PlayerState.Idle;
                    break;
                }
                currentPlayerState = inputEventChannel.IsRunning ? PlayerState.Running : PlayerState.Walking;
                RotateMovingPlayer();
                MoveOnlyForward();
                break;
            case PlayerState.Running:
                if (inputEventChannel.IsAiming)
                {
                    currentPlayerState = PlayerState.Aiming;
                    break;
                }
                if (!IsMoving())
                {
                    currentPlayerState = PlayerState.Idle;
                    break;
                }
                currentPlayerState = inputEventChannel.IsRunning ? PlayerState.Running : PlayerState.Walking;
                RotateMovingPlayer();
                MoveOnlyForward();
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Looting:
                break;
            case PlayerState.Aiming:
                if (!inputEventChannel.IsAiming)
                {
                    currentPlayerState = PlayerState.Idle;
                    break;
                }
                MoveTowardsInput();
                RotateTowardsMousePosition();
                break;
        }
    }

    private void LateUpdate()
    {
        AnimationController();
    }

    private void AnimationController()
    {
        string animationName = "";
        switch (currentPlayerState)
        {
            case PlayerState.Idle:
                animationName = "Idle";
                break;
            case PlayerState.Walking:
                animationName = "Walk";
                break;
            case PlayerState.Running:
                animationName = "Run";
                break;
            case PlayerState.Attacking:
                animationName = "Attack";
                break;
            case PlayerState.Looting:
                animationName = "Loot";
                break;
            case PlayerState.Aiming:
                animationName = "Aim";
                break;
        }

        _animationSystem.PlayAnimation(animationName);

    }

    private enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Attacking,
        Aiming,
        Looting
    }
}