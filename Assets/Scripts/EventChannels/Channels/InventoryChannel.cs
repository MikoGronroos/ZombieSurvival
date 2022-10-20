using UnityEngine;
using Finark.Events;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "EventChannels/InventoryChannel")]
public class InventoryChannel : EventChannelBase
{

    public EventChannel InventoryFull { get; set; }

    public delegate void InventorySlotClicked(int index, Item item, Vector3 pos, Action<bool, int> callback = null);

    public InventorySlotClicked InventorySlotClickedEvent { get; set; }

    public EventChannel InventoryEquip { get; set; }
    public EventChannel InventoryDequip { get; set; }

    public EventChannel InventoryConsume { get; set; }

    public EventChannelDatabaseItem FetchInventoryItemWithId { get; set; }

    #region Inventory Manipulation

    public delegate bool ItemAmountBool(Item item, int amount, Action callback = null);
    public delegate int ItemAmountInt(Item item);

    public ItemAmountBool HasAmountOfItems { get; set; }

    public ItemAmountInt GetAmountOfItems { get; set; }

    public ItemAmountBool RemoveAmountOfItems { get; set; }

    public ItemAmountBool AddAmountOfItems { get; set; }

    #endregion

    #region Looting

    public delegate void OpenedContainer(IEnumerable<ContainerSlot> items, Action<bool, int> callback);

    public OpenedContainer OpenedContainerEvent { get; set; }

    public delegate void LootItem(float time, int id);

    public LootItem LootItemEvent { get; set; }

    public delegate void ItemLooted(int id);

    public ItemLooted ItemLootedEvent { get; set; }

    #endregion

}
