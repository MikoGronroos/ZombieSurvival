using UnityEngine;
using System.Collections.Generic;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private Item item;

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
        if (inventoryChannel.TryToAddItemToInventory.Invoke(new Dictionary<string, object> { { "id", item.ItemId } }))
        {
            if (infiteItem) return;

            Destroy(gameObject);

        }
    }
}
