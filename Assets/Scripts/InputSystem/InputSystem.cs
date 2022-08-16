using UnityEngine;

public class InputSystem : MonoBehaviour
{

    [field: SerializeField] public Vector3 MoveVector { get; private set; }

    [field: SerializeField] public bool IsRunning { get; private set; }

    [field: SerializeField] public bool IsInteracting { get; private set; }

    [field: SerializeField] public bool IsAttacking { get; private set; }

    #region Singleton

    public static InputSystem Instance {
        get 
        {
            return _instance;
        }
    }

    private static InputSystem _instance;

    #endregion

    private void Awake()
    {
        _instance = this;
    }


    private void Update()
    {
        MoveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsInteracting = Input.GetMouseButtonDown(0);
        IsAttacking = Input.GetMouseButtonDown(0);
    }

}