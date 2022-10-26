using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Movement

    [Header("Movement")]
    [SerializeField] private float walkMovemenSpeed;
    [SerializeField] private float runMovemenSpeed;
    [SerializeField] private float aimMovementSpeed;

    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private bool canMove = true;
    [SerializeField] private bool useGravity = false;

    public bool CanMove { get { return canMove; } }

    [SerializeField] private Vector3 _velocity;

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

    #endregion

    #region Rotation

    [Header("Rotation")]

    [SerializeField] private float turnSmoothTime = 0.04f;

    private Vector3 _direction;

    private float _turnSmoothVelocity;

    #endregion

    #region GroundCheck

    [Header("GroundCheck")]

    [SerializeField] private float distance = 0.04f;
    [SerializeField] private LayerMask walkableMask;
    [SerializeField] private Transform groundCheckPos;

    #endregion

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InputEventChannel inputEventChannel;

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

    private void UpdateGravity()
    {
        _velocity.y += gravityValue * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheckPos.position, distance, walkableMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckPos.position, distance);
    }

    private void MoveTowardsForward()
    {
        Vector3 moveDir = inputEventChannel.MoveVector.normalized;
        controller.Move(transform.forward * (moveDir.magnitude * _currentMovementSpeed) * Time.deltaTime);
        if ((bool)interactionData.IsInteractingEvent?.Invoke() && inputEventChannel.MoveVector.magnitude != 0)
        {
            interactionData.EndInteraction?.Invoke();
        }
    }

    private void MoveTowardsInput()
    {
        Vector3 moveDir = inputEventChannel.MoveVector.normalized;
        controller.Move(moveDir * _currentMovementSpeed * Time.deltaTime);
        if ((bool)interactionData.IsInteractingEvent?.Invoke() && inputEventChannel.MoveVector.magnitude != 0)
        {
            interactionData.EndInteraction?.Invoke();
        }
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

        if (useGravity)
        {
            if (GroundCheck() && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            UpdateGravity();
        }

        if (canMove)
        {
            if (inputEventChannel.MoveVector.magnitude != 0 && !inputEventChannel.IsAiming)
            {
                _animationSystem.PlayAnimation("Walk");
                MoveTowardsForward();
            }
            else if(!inputEventChannel.IsAiming)
            {
                _animationSystem.PlayAnimation("Idle");
            }
            if (inputEventChannel.IsAiming)
            {
                MoveTowardsInput();
            }
        }
        if (inputEventChannel.IsAiming)
        {
            RotateTowardsMousePosition();
            _animationSystem.PlayAnimation("Aim");
        }
        else
        {
            RotateMovingPlayer();
        }
    }
}