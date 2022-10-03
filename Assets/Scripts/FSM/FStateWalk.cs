﻿using UnityEngine;

public class FStateWalk : FState
{

    private Vector3 _direction;

    private float _turnSmoothVelocity;

    private bool _idle;
    private bool _running;
    private bool _aiming;

    public FStateWalk(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {

    }

    public override void Enter()
    {
        _idle = false;
        _running = false;
        _aiming = false;
    }

    public override void HandleInput()
    {
        if (_inputEventChannel.MoveVector.magnitude == 0)
        {
            _idle = true;
        }
        if (_inputEventChannel.IsRunning)
        {
            _running = true;
        }
        if (_inputEventChannel.IsAiming)
        {
            _aiming = true;
        }
    }

    public override void LogicUpdate()
    {

        if (_aiming)
        {
            _stateMachine.ChangeState(_character.fStateAim);
        }

        if (_idle)
        {
            _stateMachine.ChangeState(_character.fStateIdle);
        }

        if (_running)
        {
            _stateMachine.ChangeState(_character.fStateRun);
        }

        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(_character.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _character.TurnSmoothTime);
            _character.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        _character.Controller.Move(_character.transform.forward * _character.CurrentMovementSpeed * Time.deltaTime);

        _character.AnimationSystem.PlayAnimation("Walk");

    }

}
