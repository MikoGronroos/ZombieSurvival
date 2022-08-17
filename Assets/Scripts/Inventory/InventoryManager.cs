using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private InventoryDatabase currentDatabase;

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private PlayerStatsChannel playerStatsChannel;
    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    private void OnEnable()
    {
        inventoryChannel.TryToAddItemToInventory += AddItem;
        inventoryChannel.FetchInventoryItemWithIndex += FetchInventoryItemWithIndex;
    }

    private void OnDisable()
    {
        inventoryChannel.TryToAddItemToInventory -= AddItem;
        inventoryChannel.FetchInventoryItemWithIndex -= FetchInventoryItemWithIndex;
    }

    private void Start()
    {
        SendInventoryDrawRequestToUI();
    }

    public void RemoveItem(Item item)
    {
        if (FindItemWithSpaceWithId(item.ItemId, out var data))
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

    private bool AddItem(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        Item item = itemDatabaseChannel.FetchItemFromDatabaseWithID(args);
        if (FindItemWithSpaceWithId(item.ItemId, out var data))
        {
            if (data.HasSpaceOnStack())
            {
                data.IncrementStack();
                SendInventoryDrawRequestToUI();
                return true;
            }
            else
            {
                CreateNewStack(item);
                playerStatsChannel.ChangePlayerWeight?.Invoke(new Dictionary<string, object> { { "Weight", item.Weight } });
                return true;
            }
        }
        else
        {
            if (!DatabaseIsFull())
            {
                CreateNewStack(item);
                playerStatsChannel.ChangePlayerWeight?.Invoke(new Dictionary<string, object> { { "Weight", item.Weight } });
                return true;
            }
        }
        inventoryChannel.InventoryFull?.Invoke(null);
        return false;
    }

    private bool FindItemWithSpaceWithId(int id, out DatabaseItem wrapper)
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

    private void CreateNewStack(Item item)
    {
        currentDatabase.Database.Add(new DatabaseItem(item, currentDatabase.Database.Count));
        SendInventoryDrawRequestToUI();
    }

    private bool DatabaseIsFull()
    {
        return currentDatabase.Database.Count >= currentDatabase.DatabaseMaxSize;
    }

    private DatabaseItem FetchInventoryItemWithIndex(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        return currentDatabase.Database[(int)args["Index"]];
    }

    private void SendInventoryDrawRequestToUI()
    {
        userInterfaceChannel.DrawInventory?.Invoke(new Dictionary<string, object> { { "Database", currentDatabase.Database } });
    }

}
