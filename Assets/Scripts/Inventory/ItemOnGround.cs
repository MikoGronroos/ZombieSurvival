using UnityEngine;
using System.Collections.Generic;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private Item item;
    [SerializeField] private int amountOfItems = 1;

    [Header("Item Settings")]
    [SerializeField] private bool infiteItem;

    [Header("Event Channels")]
    [SerializeField] private InventoryChannel inventoryChannel;

    public float GetInteractionTime()
    {
        return ItemPickupSpeedFormula.GetItemPickupSpeed(item.Weight);
    }

    public string GetDescription()
    {
        return $"Pickup {item.ItemName}";
    }

    public void Interact()
    {
        if ((bool)inventoryChannel.AddAmountOfItems?.Invoke(item, amountOfItems))
        {
            if (infiteItem) return;

            Destroy(gameObject);

        }
    }
}
