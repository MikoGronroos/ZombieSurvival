using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class InventorySlotClicked : MonoBehaviour
{

    [SerializeField] private int index;

    [SerializeField] private GameObject inventorySlotClickedPanel;
    [SerializeField] private Item currentItem;

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private InteractionData interactionData;

    private Action<bool, int> _callback;
    private InventoryDelay inventoryDelay;

    public int Index { get { return index; } set { index = value; } }

    public Item CurrentItem { get { return currentItem; } set { currentItem = value; } }

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

    public Action<bool, int> Callback { get { return _callback; } set { _callback = value; } }

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
        inventoryChannel.InventoryConsume?.Invoke(new Dictionary<string, object> { { "Index", index } });
        TogglePanel(false);
    }

    public void Loot()
    {
        StartCoroutine(ClickActionCoroutine(ItemPickupSpeedFormula.GetItemPickupSpeed(currentItem.Weight), ()=> {
            _callback?.Invoke(false, index);
            TogglePanel(false);
        }));
    }

    public void LootAll()
    {
        _callback?.Invoke(true, 0);
        TogglePanel(false);
    }

    private IEnumerator ClickActionCoroutine(float time, Action callback)
    {
        Timer timer = new Timer(time, null);
        interactionData.StartProgressBarEvent?.Invoke(time);
        while (timer.CurrentTime < timer.MaxTime)
        {
            timer.Tick();
            yield return null;
        }
        callback?.Invoke();
    }

}