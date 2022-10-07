using UnityEngine;

public class FStateAim: FState
{

    private bool _aiming;
    private bool _moving;
    private bool _reloading;

    public FStateAim(FCharacter character, FStateMachine stateMachine, InputEventChannel inputEventChannel) : base(character, stateMachine, inputEventChannel)
    {

    }

    public override void Enter()
    {
        _aiming = true;
        _moving = false;
        _reloading = false;
    }

    public override void HandleInput()
    {
        if (!_inputEventChannel.IsAiming)
        {
            _aiming = false;
        }

        _reloading = _inputEventChannel.IsReloading;

        _moving = _inputEventChannel.MoveVector.magnitude != 0;

    }

    public override void LogicUpdate()
    {

        if (_reloading)
        {
            _stateMachine.ChangeState(_character.fStateReload);
        }

        if (!_aiming)
        {
            _stateMachine.ChangeState(_character.fStateIdle);
        }

        Ray cameraRay = _character.Camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            _character.transform.LookAt(new Vector3(pointToLook.x, _character.transform.position.y, pointToLook.z));
        }

        if (_moving)
        {
            _character.Controller.Move(_inputEventChannel.MoveVector.normalized * _character.CurrentMovementSpeed * Time.deltaTime);
        }

        _character.AnimationSystem.PlayAnimation("Aim");

    }
}