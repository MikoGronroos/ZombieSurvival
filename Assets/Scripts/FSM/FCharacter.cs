using System;
using UnityEngine;

public class FCharacter : MonoBehaviour
{

    #region Movement

    [Header("Movement")]
    [SerializeField] private float walkMovemenSpeed;
    [SerializeField] private float runMovemenSpeed;
    [SerializeField] private float aimMovementSpeed;

    public float CurrentMovementSpeed
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

    public float TurnSmoothTime { get { return turnSmoothTime; } }

    #endregion

    #region References

    [Header("References")]

    [SerializeField] private WeaponController weaponController;
    [SerializeField] private CharacterController controller;
    [SerializeField] private InputEventChannel inputEventChannel;
    [SerializeField] private PlayerEventChannel playerEventChannel;
    [SerializeField] private Animator animator;

    public WeaponController WeaponController { get { return weaponController; } }
    public CharacterController Controller { get { return controller; } }

    #endregion

    private FStateMachine _stateMachine = new FStateMachine();
    private AnimationSystem _animationSystem = new AnimationSystem();
    private Camera _camera;

    public AnimationSystem AnimationSystem { get { return _animationSystem; } }

    public Camera Camera { get { return _camera; } }

    #region States

    public FStateIdle fStateIdle { get; private set; }
    public FStateWalk fStateWalk { get; private set; }
    public FStateRun fStateRun { get; private set; }
    public FStateAim fStateAim { get; private set; }
    public FStateAttack fStateAttack { get; private set; }
    public FStateReload fStateReload { get; private set; }
    public FStateDead fStateDead { get; private set; }

    #endregion

    private void Awake()
    {
        _camera = Camera.main;
        fStateIdle = new FStateIdle(this, _stateMachine, inputEventChannel);
        fStateWalk = new FStateWalk(this, _stateMachine, inputEventChannel);
        fStateRun = new FStateRun(this, _stateMachine, inputEventChannel);
        fStateAim = new FStateAim(this, _stateMachine, inputEventChannel);
        fStateAttack = new FStateAttack(this, _stateMachine, inputEventChannel);
        fStateReload = new FStateReload(this, _stateMachine, inputEventChannel);
        fStateDead = new FStateDead(this, _stateMachine, inputEventChannel);
    }

    private void Start()
    {
        _stateMachine.Initialize(fStateIdle);
        _animationSystem.SetupAnimationSystem(animator);
    }

    private void OnEnable()
    {
        playerEventChannel.DeadEvent += DeadEventListener;
    }

    private void OnDisable()
    {
        playerEventChannel.DeadEvent -= DeadEventListener;
    }

    private void Update()
    {
        _stateMachine.currentState.HandleInput();

        _stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.currentState.PhysicsUpdate();
    }

    private void DeadEventListener()
    {
        _stateMachine.ChangeState(fStateDead);
    }

}
