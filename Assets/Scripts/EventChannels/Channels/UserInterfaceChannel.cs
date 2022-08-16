using Finark.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/UserInterfaceChannel")]
public class UserInterfaceChannel : EventChannelBase
{

    #region Interaction

    public EventChannel ToggleMouseOnTopOfInteractionUI;

    #endregion

    #region Inventory

    public EventChannel DrawInventory;

    #endregion

}