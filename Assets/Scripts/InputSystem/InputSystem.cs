using UnityEngine;
using Finark.Utils;

public class InputSystem : MonoBehaviour
{

    [SerializeField] private InputEventChannel inputEventChannel;

    private void Update()
    {

        inputEventChannel.MoveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (!MyUtils.IsPointerOverUI())
        {
            inputEventChannel.IsAttacking = Input.GetMouseButton(0);
            if (Input.GetKeyDown(KeyCode.F)) inputEventChannel.IsInteracting?.Invoke();
        }
        inputEventChannel.IsReloading = Input.GetKeyDown(KeyCode.R);
        inputEventChannel.IsAiming = Input.GetKey(KeyCode.LeftControl);
        inputEventChannel.IsRunning = Input.GetKey(KeyCode.LeftShift);
    }
}