using UnityEngine;
using System.Collections.Generic;
using System;

public class InventorySlotClicked : MonoBehaviour
{

    [SerializeField] private int index;

    [SerializeField] private GameObject inventorySlotClickedPanel;

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private InteractionData interactionData;

    private Action<Dictionary<string, object>> _callback;
    private InventorySlotUI inventorySlotUI;

    public int Index { get { return index; } set { index = value; } }
    public InventorySlotUI InventorySlotUI {
        get
        { 
            return inventorySlotUI;
        } 
        set
        { 
            inventorySlotUI = value;
            interactionData.InteractedEvent?.Invoke(inventorySlotUI);
        } 
    }
    public Action<Dictionary<string, object>> Callback { get { return _callback; } set { _callback = value; } }

    public void TogglePanel(bool value)
    {
        inventorySlotClickedPanel.SetActive(value);
    }

    public void Equip()
    {
        inventoryChannel.InventoryEquip?.Invoke(new Dictionary<string, object> { { "Index", index } });
        TogglePanel(false);
    }

    public void Dequip()
    {
        inventoryChannel.InventoryDequip?.Invoke(new Dictionary<string, object> { { "Index", index } });
        TogglePanel(false);
    }

    public void Eat()
    {
        inventoryChannel.InventoryEat?.Invoke(new Dictionary<string, object> { { "Index", index } });
        TogglePanel(false);
    }

    public void Loot()
    {
        _callback?.Invoke(new Dictionary<string, object> { { "Index", index }, { "FullLooting", false } });
        TogglePanel(false);
    }

    public void LootAll()
    {
        _callback?.Invoke(new Dictionary<string, object> { { "FullLooting", true } });
        TogglePanel(false);
    }

}