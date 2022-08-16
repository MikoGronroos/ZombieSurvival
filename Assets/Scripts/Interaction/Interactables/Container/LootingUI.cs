using System;
using System.Collections.Generic;
using UnityEngine;

public class LootingUI : MonoBehaviour
{

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryItemParent;

    private List<GameObject> drawnGameObjects = new List<GameObject>();

    private void OnEnable()
    {
        inventoryChannel.OpenedContainer += OnOpenedContainerListener;
    }

    private void OnDisable()
    {
        inventoryChannel.OpenedContainer -= OnOpenedContainerListener;
    }

    private void OnOpenedContainerListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        IEnumerable<Item> items = (IEnumerable<Item>)args["items"];

        EraseDrawnItems();
        int index = 0;
        foreach (var item in items)
        {
            GameObject go = Instantiate(inventoryItemPrefab, inventoryItemParent);
            if (go.TryGetComponent(out InventorySlotUI slot))
            {
                slot.SetupSlot(item.ItemIcon, $"{item.ItemName}", "", index, callback);
            }
            drawnGameObjects.Add(go);
            index++;
        }

    }

    private void EraseDrawnItems()
    {
        foreach (var item in drawnGameObjects)
        {
            Destroy(item);
        }
    }

}
