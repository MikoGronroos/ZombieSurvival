using UnityEngine;
using System;

[CreateAssetMenu(menuName = "EventChannels/Input")]
public class InputEventChannel : ScriptableObject
{

    public bool IsRunning { get; set; }

    public bool IsAiming { get; set; }

    public bool IsAttacking { get; set; }
    public bool IsHoldingDownAttack { get; set; }

    public Action IsInteracting { get; set; }

    public bool IsReloading { get; set; }

    public Vector3 MoveVector { get; set; }


}