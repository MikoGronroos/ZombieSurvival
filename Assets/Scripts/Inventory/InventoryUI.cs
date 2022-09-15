using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private GameObject inventorySlotPrefab;

    [Header("Inventory Full")]

    [SerializeField] private GameObject inventoryFullTextGameObject;
    [SerializeField] private float inventoryFullTextTime;

    [Header("Inventory Item Clicked GameObject")]

    [SerializeField] private InventorySlotClicked inventoryItemClickedPopup;

    [Header("EventChannels")]

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private InventoryChannel inventoryChannel;

    [SerializeField] private List<GameObject> drawnSlots = new List<GameObject>();

    private void OnEnable()
    {
        userInterfaceChannel.DrawInventory += OnDrawInventoryListener;
        inventoryChannel.InventoryFull += InventoryFullPopupInvoke;
        inventoryChannel.InventorySlotClickedEvent += InventoryItemClicked;
    }

    private void OnDisable()
    {
        userInterfaceChannel.DrawInventory -= OnDrawInventoryListener;
        inventoryChannel.InventoryFull -= InventoryFullPopupInvoke;
        inventoryChannel.InventorySlotClickedEvent -= InventoryItemClicked;
    }

    private void OnDrawInventoryListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        List<DatabaseItem> database = (List<DatabaseItem>)args["Database"];

        EraseInventoryUI();

        foreach (var item in database)
        {
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventorySlotParent);
            drawnSlots.Add(newSlot);
            if (newSlot.TryGetComponent(out InventorySlotUI slot))
            {
                slot.SetupSlot(item.Item.ItemIcon, $"{item.Item.ItemName} x{item.CurrentStackSize}", $"", item.ItemIndexInDatabase, item.Item);
            }
        }
    }

    private void EraseInventoryUI()
    {
        foreach (var slot in drawnSlots)
        {
            Destroy(slot);
        }
        drawnSlots.Clear();
    }

    #region Inventory Full

    private void InventoryFullPopupInvoke(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        StartCoroutine(InventoryFullPopup());
    }

    IEnumerator InventoryFullPopup()
    {
        inventoryFullTextGameObject.SetActive(true);
        yield return new WaitForSeconds(inventoryFullTextTime);
        inventoryFullTextGameObject.SetActive(false);
    }

    #endregion

    private void InventoryItemClicked(int index, Item item, Vector3 pos, InventoryDelay delay, Action<bool, int> callback)
    {
        inventoryItemClickedPopup.Index = index;
        inventoryItemClickedPopup.Callback = callback;
        inventoryItemClickedPopup.InventoryDelay = delay;
        inventoryItemClickedPopup.transform.position = pos;
        inventoryItemClickedPopup.CurrentItem = item;
        inventoryItemClickedPopup.TogglePanel(true);
    }

}
