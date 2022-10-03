using System;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{

    [SerializeField] private string containerName;

    [SerializeField] private float interactionTime;

    [SerializeField] private List<Item> containerItems = new List<Item>();

    [SerializeField] private InventoryChannel inventoryChannel;

    public string GetDescription()
    {
        return $"Open {containerName}";
    }

    public float GetInteractionTime()
    {
        return interactionTime;
    }

    public void Interact()
    {
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

}
