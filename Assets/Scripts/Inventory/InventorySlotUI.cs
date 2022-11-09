using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlotUI : InventoryDelay, IPointerClickHandler
{

    [SerializeField] private Image spriteRenderer;
    [SerializeField] private TextMeshProUGUI itemNameAndStackSizeText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private int inventoryId;
    [SerializeField] private Item currentItem;
    [SerializeField] private SlotMenuType type;

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private InteractionData interactionData;

    private Action<bool, int> _callback;

    public void SetupSlot(Sprite icon, string itemNameAndStackSizeText, string stateText, int inventoryId, Item item, SlotMenuType type, Action<bool, int> callback = null)
    {
        spriteRenderer.sprite = icon;
        this.itemNameAndStackSizeText.text = itemNameAndStackSizeText;
        this.stateText.text = stateText;
        this.inventoryId = inventoryId;
        currentItem = item;
        this.type = type;
        _callback = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryChannel.InventorySlotClickedEvent?.Invoke(inventoryId, currentItem, transform.position, type, _callback);
    }

}
