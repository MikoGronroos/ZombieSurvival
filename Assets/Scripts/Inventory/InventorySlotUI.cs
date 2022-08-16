using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Image spriteRenderer;
    [SerializeField] private TextMeshProUGUI itemNameAndStackSizeText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private int inventoryIndex;

    [SerializeField] private InventoryChannel inventoryChannel;

    public int InventoryIndex { get { return inventoryIndex; } private set { } }

    public void SetupSlot(Sprite icon, string itemNameAndStackSizeText, string stateText, int inventoryIndex)
    {
        spriteRenderer.sprite = icon;
        this.itemNameAndStackSizeText.text = itemNameAndStackSizeText;
        this.stateText.text = stateText;
        this.inventoryIndex = inventoryIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryChannel.InventorySlotClicked?.Invoke(new Dictionary<string, object> { { "Index", inventoryIndex }, { "Position", transform.position } });
    }

}
