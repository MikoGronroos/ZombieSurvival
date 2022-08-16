using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{

    [SerializeField] private List<Item> containerItems = new List<Item>();

    [SerializeField] private InventoryChannel inventoryChannel;

    public string GetDescription()
    {
        return "Open container";
    }

    public void Interact()
    {

        inventoryChannel.OpenedContainer?.Invoke(new Dictionary<string, object> { { "items", containerItems } });

    }
}
