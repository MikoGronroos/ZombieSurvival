using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootingUI : MonoBehaviour
{

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryItemParent;

    [SerializeField] private Button closeLootingUIButton;

    private List<GameObject> drawnGameObjects = new List<GameObject>();

    private void OnEnable()
    {
        inventoryChannel.OpenedContainerEvent += OnOpenedContainerListener;
    }

    private void OnDisable()
    {
        inventoryChannel.OpenedContainerEvent -= OnOpenedContainerListener;
    }

    private void OnOpenedContainerListener(IEnumerable<Item> items, Action<bool,int> callback)
    {

        EraseDrawnItems();
        int index = 0;
        foreach (var item in items)
        {
            GameObject go = Instantiate(inventoryItemPrefab, inventoryItemParent);
            if (go.TryGetComponent(out InventorySlotUI slot))
            {
                slot.SetupSlot(item.ItemIcon, $"{item.ItemName}", "", index, item, callback);
            }
            drawnGameObjects.Add(go);
            index++;
        }


        inventoryItemParent.gameObject.SetActive(true);

        closeLootingUIButton.onClick.AddListener(() => {
            inventoryItemParent.gameObject.SetActive(false);
            closeLootingUIButton.onClick.RemoveAllListeners();
        });

    }

    private void EraseDrawnItems()
    {
        foreach (var item in drawnGameObjects)
        {
            Destroy(item);
        }
    }

}
