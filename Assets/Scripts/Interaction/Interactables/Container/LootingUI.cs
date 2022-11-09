using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootingUI : MonoBehaviour
{

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryItemParent;

    [SerializeField] private Button closeLootingUIButton;

    [SerializeField] private InteractionData interactionData;

    [SerializeField] private List<InventorySlotUI> drawnGameObjects = new List<InventorySlotUI>();

    private void OnEnable()
    {
        inventoryChannel.OpenedContainerEvent += OnOpenedContainerListener;
        inventoryChannel.LootItemEvent += Loot;
        inventoryChannel.ItemLootedEvent += EraseDrawnItemWithIndex;
    }

    private void OnDisable()
    {
        inventoryChannel.OpenedContainerEvent -= OnOpenedContainerListener;
        inventoryChannel.LootItemEvent -= Loot;
        inventoryChannel.ItemLootedEvent -= EraseDrawnItemWithIndex;
    }

    private void OnOpenedContainerListener(IEnumerable<ContainerSlot> items, Action<bool,int> callback)
    {

        EraseDrawnItems();
        foreach (var item in items)
        {
            GameObject go = Instantiate(inventoryItemPrefab, inventoryItemParent);
            if (go.TryGetComponent(out InventorySlotUI slot))
            {
                slot.SetupSlot(item.Item.ItemIcon, $"{item.Item.ItemName}", "", item.Id, item.Item, SlotMenuType.ContainerItem, callback);
                drawnGameObjects.Add(slot);
            }
        }


        inventoryItemParent.gameObject.SetActive(true);

        closeLootingUIButton.onClick.AddListener(() => {
            inventoryItemParent.gameObject.SetActive(false);
            closeLootingUIButton.onClick.RemoveAllListeners();
        });

    }

    private void EraseDrawnItems()
    {
        for (int i = drawnGameObjects.Count - 1; i >= 0; i--)
        {
            Destroy(drawnGameObjects[i].gameObject);
        }
        drawnGameObjects.Clear();
    }
    
    private void EraseDrawnItemWithIndex(int index)
    {
        var gameObject = drawnGameObjects[index];
        drawnGameObjects.Remove(gameObject);
        Destroy(gameObject.gameObject);
    }

    private void Loot(float time, int index)
    {
        interactionData.StartProgressBarEvent?.Invoke(time, drawnGameObjects[index]);
    }
}
