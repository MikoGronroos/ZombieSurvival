using UnityEngine;
using Finark.Events;

[CreateAssetMenu(menuName = "EventChannels/InventoryChannel")]
public class InventoryChannel : EventChannelBase
{

    public EventChannelBool TryToAddItemToInventory;

    public EventChannel InventoryFull;

    public EventChannel InventorySlotClicked;

    public EventChannel InventoryEquip;
    public EventChannel InventoryDequip;
    public EventChannel InventoryEat;

    public EventChannelDatabaseItem FetchInventoryItemWithIndex;

    #region Looting

    public EventChannel OpenedContainer;

    #endregion

}
