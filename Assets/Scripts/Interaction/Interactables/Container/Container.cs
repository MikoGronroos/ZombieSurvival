using System;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable, ISaveable
{

    [SerializeField] private string containerName;

    [SerializeField] private float interactionTime;

    [SerializeField] private bool hasBeenOpened;

    [SerializeField] private List<Item> containerItems = new List<Item>();

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    public string GetDescription()
    {
        return $"Open {containerName}";
    }

    public float GetInteractionTime()
    {
        return hasBeenOpened ? 0 : interactionTime;
    }

    public void Interact()
    {
        hasBeenOpened = true;
        inventoryChannel.OpenedContainerEvent?.Invoke(containerItems, ItemLooting);
    }

    private void ItemLooting(bool fullLooting, int index)
    {
        if (fullLooting)
        {
            for (int i = containerItems.Count - 1; i >= 0; i--)
            {
                LootItem(i);
            }
        }
        else
        {
            LootItem(index);
        }
    }

    private void LootItem(int index)
    {
        inventoryChannel.AddAmountOfItems?.Invoke(containerItems[index], 1, ()=> {
            RemoveItemWithIndex(index);
            inventoryChannel.OpenedContainerEvent?.Invoke(containerItems, ItemLooting);
        });
    }

    private void RemoveItemWithIndex(int index)
    {
        containerItems.RemoveAt(index);
    }

    public void RestoreState(object state)
    {
        var data = (SaveData)state;
        hasBeenOpened = data.hasBeenOpened;

        containerItems.Clear();
        foreach (var item in data.items)
        {
            containerItems.Add(itemDatabaseChannel.FetchItemFromDatabaseWithID?.Invoke(item));
        }
        inventoryChannel.OpenedContainerEvent?.Invoke(containerItems, ItemLooting);
    }

    public object CaptureState()
    {
        List<int> ids = new List<int>();

        foreach (var item in containerItems)
        {
            ids.Add(item.ItemId);
        }

        return new SaveData {
            hasBeenOpened = hasBeenOpened,
            items = ids
        };
    }

    [Serializable]
    public struct SaveData
    {
        public bool hasBeenOpened;
        public List<int> items;
    }



}
