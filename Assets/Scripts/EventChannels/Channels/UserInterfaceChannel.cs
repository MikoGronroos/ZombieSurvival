using Finark.Events;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/UserInterfaceChannel")]
public class UserInterfaceChannel : EventChannelBase
{

    #region Interaction

    public EventChannel ToggleMouseOnTopOfInteractionUI { get; set; }

    public Action<bool> ToggleGeneralInteractionDelayUI { get; set; }

    public delegate void Interacted(float time);

    public Interacted InteractedEvent { get; set; } 

    #endregion

    #region Inventory

    public EventChannel DrawInventory;

    #endregion

}