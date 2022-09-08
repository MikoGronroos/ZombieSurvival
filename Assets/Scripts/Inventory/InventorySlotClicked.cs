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
    private InventoryDelay inventoryDelay;

    public int Index { get { return index; } set { index = value; } }
    public InventoryDelay InventoryDelay
    {
        get
        { 
            return inventoryDelay;
        } 
        set
        {
            inventoryDelay = value;
            interactionData.InteractedEvent?.Invoke(inventoryDelay);
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