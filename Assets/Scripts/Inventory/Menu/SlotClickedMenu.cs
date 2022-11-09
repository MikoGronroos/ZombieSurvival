using System;
using System.Collections.Generic;
using UnityEngine;

public class SlotClickedMenu : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] private GameObject inventorySlotClickedPanel;
    [SerializeField] private Item currentItem;

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private InteractionData interactionData;

    [SerializeField] private SlotClickedMenuButton[] buttons;

    private Action<bool, int> _callback;

    public int Id { get { return id; } set { id = value; } }

    public Item CurrentItem { get { return currentItem; } set { currentItem = value; } }

    public Action<bool, int> Callback { get { return _callback; } set { _callback = value; } }

    public void FilterMenu(SlotMenuType menu)
    {

        foreach (var button in buttons)
        {
            if (button.SlotMenuType == menu)
            {
                button.ToggleGameObject(true);
            }
            else
            {
                button.ToggleGameObject(false);
            }
        }
    }

    public void TogglePanel(bool value)
    {
        inventorySlotClickedPanel.SetActive(value);
    }

    public void Equip()
    {
        inventoryChannel.InventoryEquip?.Invoke(new Dictionary<string, object> { { "Id", id } });
        TogglePanel(false);
    }

    public void Dequip()
    {
        inventoryChannel.InventoryDequip?.Invoke(new Dictionary<string, object> { { "Id", id } });
        TogglePanel(false);
    }

    public void Eat()
    {
        inventoryChannel.InventoryConsume?.Invoke(new Dictionary<string, object> { { "Id", id } });
        TogglePanel(false);
    }

    public void Loot()
    {
        _callback?.Invoke(false, id);
        TogglePanel(false);
    }

    public void LootAll()
    {
        _callback?.Invoke(true, 0);
        TogglePanel(false);
    }

}

public enum SlotMenuType
{
    InInventory,
    ContainerItem
}