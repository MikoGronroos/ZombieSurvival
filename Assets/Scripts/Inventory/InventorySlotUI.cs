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
    [SerializeField] private int inventoryIndex;
    [SerializeField] private Item currentItem;

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private InteractionData interactionData;

    private Action<bool, int> _callback;

    public int InventoryIndex { get { return inventoryIndex; } private set { } }

    public void SetupSlot(Sprite icon, string itemNameAndStackSizeText, string stateText, int inventoryIndex, Item item, Action<bool, int> callback = null)
    {
        spriteRenderer.sprite = icon;
        this.itemNameAndStackSizeText.text = itemNameAndStackSizeText;
        this.stateText.text = stateText;
        this.inventoryIndex = inventoryIndex;
        currentItem = item;
        _callback = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryChannel.InventorySlotClickedEvent?.Invoke(inventoryIndex, currentItem, transform.position, this, _callback);
    }

}
