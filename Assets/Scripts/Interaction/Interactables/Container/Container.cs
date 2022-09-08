using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{

    [SerializeField] private string containerName;

    [SerializeField] private List<Item> containerItems = new List<Item>();

    [SerializeField] private InventoryChannel inventoryChannel;

    public string GetDescription()
    {
        return $"Open {containerName}";
    }

    public void Interact()
    {
        inventoryChannel.OpenedContainer?.Invoke(new Dictionary<string, object> { { "items", containerItems } }, ItemLooting);
    }

    private void ItemLooting(Dictionary<string, object> args)
    {
        bool fullLooting = (bool)args["FullLooting"];
        if (fullLooting)
        {
            for (int i = containerItems.Count - 1; i >= 0; i--)
            {
                LootItem(i);
            }
        }
        else
        {
            int index = (int)args["Index"];
            LootItem(index);
        }
    }

    public void LootItem(int index)
    {
        inventoryChannel.TryToAddItemToInventory?.Invoke(new Dictionary<string, object> { { "id", containerItems[index].ItemId } }, (Dictionary<string, object> args) => {
            RemoveItemWithIndex(index);
            inventoryChannel.OpenedContainer?.Invoke(new Dictionary<string, object> { { "items", containerItems } }, ItemLooting);
        });
    }

    private void RemoveItemWithIndex(int index)
    {
        containerItems.RemoveAt(index);
    }

}
