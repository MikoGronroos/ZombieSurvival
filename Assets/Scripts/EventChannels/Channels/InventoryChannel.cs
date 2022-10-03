using UnityEngine;
using Finark.Events;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "EventChannels/InventoryChannel")]
public class InventoryChannel : EventChannelBase
{

    public EventChannel InventoryFull { get; set; }

    public delegate void InventorySlotClicked(int index, Item item, Vector3 pos, InventoryDelay delay, Action<bool, int> callback = null);

    public InventorySlotClicked InventorySlotClickedEvent { get; set; }

    public EventChannel InventoryEquip { get; set; }
    public EventChannel InventoryDequip { get; set; }

    public EventChannel InventoryEat { get; set; }

    public EventChannelDatabaseItem FetchInventoryItemWithIndex { get; set; }

    #region Inventory Manipulation

    public delegate bool ItemAmount(Item item, int amount, Action callback = null);

    public ItemAmount HasAmountOfItems { get; set; }

    public ItemAmount RemoveAmountOfItems { get; set; }

    public ItemAmount AddAmountOfItems { get; set; }

    #endregion

    #region Looting

    public delegate void OpenedContainer(IEnumerable<Item> items, Action<bool, int> callback);

    public OpenedContainer OpenedContainerEvent { get; set; }

    #endregion

}
