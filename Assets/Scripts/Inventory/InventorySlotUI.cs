using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class InventorySlotUI : InventoryDelay, IPointerClickHandler
{

    [SerializeField] private Image spriteRenderer;
    [SerializeField] private TextMeshProUGUI itemNameAndStackSizeText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private int inventoryIndex;

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private InteractionData interactionData;

    private Action<Dictionary<string, object>> _callback;

    public int InventoryIndex { get { return inventoryIndex; } private set { } }

    public void SetupSlot(Sprite icon, string itemNameAndStackSizeText, string stateText, int inventoryIndex, Action<Dictionary<string, object>> callback = null)
    {
        spriteRenderer.sprite = icon;
        this.itemNameAndStackSizeText.text = itemNameAndStackSizeText;
        this.stateText.text = stateText;
        this.inventoryIndex = inventoryIndex;
        _callback = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryChannel.InventorySlotClicked?.Invoke(new Dictionary<string, object> { { "Index", inventoryIndex }, { "Position", transform.position }, { "Slot", this } }, _callback);
    }

}
