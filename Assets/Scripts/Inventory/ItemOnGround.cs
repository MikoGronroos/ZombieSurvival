using UnityEngine;
using System.Collections.Generic;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private int itemId;

    [Header("Item Settings")]
    [SerializeField] private bool infiteItem;

    [Header("Event Channels")]
    [SerializeField] private InventoryChannel inventoryChannel;

    public string GetDescription()
    {
        return "Pickup the item.";
    }

    public void Interact()
    {
        if (inventoryChannel.TryToAddItemToInventory.Invoke(new Dictionary<string, object> { { "id", itemId } }))
        {

            if (infiteItem) return;

            Destroy(gameObject);

        }
    }
}
