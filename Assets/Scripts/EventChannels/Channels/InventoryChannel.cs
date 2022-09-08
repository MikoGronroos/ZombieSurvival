using UnityEngine;
using Finark.Events;

[CreateAssetMenu(menuName = "EventChannels/InventoryChannel")]
public class InventoryChannel : EventChannelBase
{

    public EventChannelBool TryToAddItemToInventory { get; set; }

    public EventChannel InventoryFull { get; set; }

    public EventChannel InventorySlotClicked { get; set; }

    public EventChannel InventoryEquip { get; set; }
    public EventChannel InventoryDequip { get; set; }
    public EventChannel InventoryEat { get; set; }

    public EventChannelDatabaseItem FetchInventoryItemWithIndex { get; set; }

    #region Looting

    public EventChannel OpenedContainer { get; set; }

    #endregion

}
