using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private InventoryDatabase currentDatabase;

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private PlayerStatsChannel playerStatsChannel;
    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    [SerializeField] private InteractionData interactionData;

    private void OnEnable()
    {
        inventoryChannel.AddAmountOfItems += AddItem;
        inventoryChannel.RemoveAmountOfItems += RemoveItem;
        inventoryChannel.HasAmountOfItems += HasAmountOfItems;
        inventoryChannel.FetchInventoryItemWithId += FetchInventoryItemWithId;
        inventoryChannel.GetAmountOfItems += GetAmountOfItems;
    }

    private void OnDisable()
    {
        inventoryChannel.AddAmountOfItems -= AddItem;
        inventoryChannel.RemoveAmountOfItems -= RemoveItem;
        inventoryChannel.HasAmountOfItems -= HasAmountOfItems;
        inventoryChannel.FetchInventoryItemWithId -= FetchInventoryItemWithId;
        inventoryChannel.GetAmountOfItems -= GetAmountOfItems;
    }

    private void Start()
    {
        SendInventoryDrawRequestToUI();
    }

    public bool RemoveItem(Item item, int amount, Action callback)
    {
        for (int i = 0; i < amount; i++)
        {
            if (FindItem(item, out var data))
            {
                if (data.IsLastItemOnStack())
                {
                    currentDatabase.Database.Remove(data);
                }
                else
                {
                    data.DecrementStack();
                }
                playerStatsChannel.ChangePlayerWeight?.Invoke(new Dictionary<string, object> { { "Weight", -item.Weight } });
                SendInventoryDrawRequestToUI();
            }
        }
        return false;
    }

    private bool AddItem(Item item, int amount, Action callback)
    {
        for (int i = 0; i < amount; i++)
        {
            if (DatabaseIsFull())
            {
                inventoryChannel.InventoryFull?.Invoke(null);
            }
            else
            {
                if (FindItemWithSpaceWithId(item.ItemId, out var data))
                {
                    if (data.HasSpaceOnStack())
                    {
                        data.IncrementStack();
                    }
                    else
                    {
                        CreateNewStack(item);
                    }
                }
                else
                {
                    CreateNewStack(item);
                }
                playerStatsChannel.ChangePlayerWeight?.Invoke(new Dictionary<string, object> { { "Weight", item.Weight } });
                SendInventoryDrawRequestToUI();
            }
        }
        callback?.Invoke();
        return false;
    }

    private bool HasAmountOfItems(Item item, int amount, Action callback)
    {
        foreach (var curItem in currentDatabase.Database)
        {
            if (curItem.Item == item && curItem.CurrentStackSize >= amount)
            {
                return true;
            }
        }
        return false;
    }

    private int GetAmountOfItems(Item item)
    {
        foreach (var curItem in currentDatabase.Database)
        {
            if (curItem.Item == item)
            {
                return curItem.CurrentStackSize;
            }
        }
        return 0;
    }

    private bool FindItemWithSpaceWithId(string id, out DatabaseItem wrapper)
    {
        foreach (var item in currentDatabase.Database)
        {

            if (!item.HasSpaceOnStack()) continue;

            if (item.Item.ItemId == id)
            {
                wrapper = item;
                return true;
            }
        }
        wrapper = null;
        return false;
    }

    private bool FindItem(Item item, out DatabaseItem wrapper)
    {
        foreach (var curItem in currentDatabase.Database)
        {
            if (curItem.Item.ItemId == item.ItemId)
            {
                wrapper = curItem;
                return true;
            }
        }
        wrapper = null;
        return false;
    }

    private void CreateNewStack(Item item)
    {
        currentDatabase.Database.Add(new DatabaseItem(item));
        SendInventoryDrawRequestToUI();
    }

    private bool DatabaseIsFull()
    {
        return currentDatabase.Database.Count >= currentDatabase.DatabaseMaxSize;
    }

    private DatabaseItem FetchInventoryItemWithId(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        int id = (int)args["Id"];
        foreach (var item in currentDatabase.Database)
        {
            if (item.SlotId == id)
            {
                return item;
            }
        }
        return null;
    }

    private void SendInventoryDrawRequestToUI()
    {
        userInterfaceChannel.DrawInventory?.Invoke(new Dictionary<string, object> { { "Database", currentDatabase.Database } });
    }

}
