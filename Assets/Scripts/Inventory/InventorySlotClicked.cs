using UnityEngine;
using System.Collections.Generic;
using System;

public class InventorySlotClicked : MonoBehaviour
{

    [SerializeField] private int index;
    [SerializeField] private InventoryChannel inventoryChannel;

    private Action<Dictionary<string, object>> _callback;

    public int Index { get { return index; } set { index = value; } }
    public Action<Dictionary<string, object>> Callback { get { return _callback; } set { _callback = value; } }

    public void Equip()
    {
        inventoryChannel.InventoryEquip?.Invoke(new Dictionary<string, object> { { "Index", index } });
        gameObject.SetActive(false);
    }

    public void Dequip()
    {
        inventoryChannel.InventoryDequip?.Invoke(new Dictionary<string, object> { { "Index", index } });
        gameObject.SetActive(false);
    }

    public void Eat()
    {
        inventoryChannel.InventoryEat?.Invoke(new Dictionary<string, object> { { "Index", index } });
        gameObject.SetActive(false);
    }

    public void Loot()
    {
        _callback?.Invoke(new Dictionary<string, object> { { "Index", index }, { "FullLooting", false } });
        gameObject.SetActive(false);
    }

    public void LootAll()
    {
        _callback?.Invoke(new Dictionary<string, object> { { "FullLooting", true } });
        gameObject.SetActive(false);
    }

}