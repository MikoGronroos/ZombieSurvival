using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Container : MonoBehaviour, IInteractable, ISaveable
{

    [SerializeField] private string containerName;

    [SerializeField] private float interactionTime;

    [SerializeField] private bool hasBeenOpened;

    [SerializeField] private List<ContainerSlot> containerItems = new List<ContainerSlot>();

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    public string GetDescription()
    {
        return $"Open {containerName}";
    }

    public float GetInteractionTime()
    {
        return hasBeenOpened ? 0 : interactionTime;
    }

    public void Interact()
    {
        hasBeenOpened = true;
        inventoryChannel.OpenedContainerEvent?.Invoke(containerItems, ItemLooting);
    }

    private int GetSlotIndexWithId(int id)
    {
        int index = 0;
        foreach (var item in containerItems)
        {
            if (item.Id == id)
            {
                return index;
            }
            index++;
        }
        return -1;
    }

    private void ItemLooting(bool fullLooting, int id)
    {
        if (fullLooting)
        {
            StartCoroutine(FullLoot());
        }
        else
        {
            int index = GetSlotIndexWithId(id);
            StartCoroutine(LootingCoroutine(ItemPickupSpeedFormula.GetItemPickupSpeed(containerItems[index].Item.Weight), index,() => {
                LootItem(index);
                inventoryChannel.ItemLootedEvent?.Invoke(index);
            }));

        }
    }

    private IEnumerator FullLoot()
    {
        for (int i = containerItems.Count - 1; i >= 0; i--)
        {
            StartCoroutine(LootingCoroutine(ItemPickupSpeedFormula.GetItemPickupSpeed(containerItems[i].Item.Weight), i, () => {
                LootItem(i);
                inventoryChannel.ItemLootedEvent?.Invoke(i);
            }));
            yield return new WaitForSeconds(ItemPickupSpeedFormula.GetItemPickupSpeed(containerItems[i].Item.Weight));
        }
    }

    private IEnumerator LootingCoroutine(float time, int index, Action callback)
    {
        inventoryChannel.LootItemEvent?.Invoke(time, index);
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    private void LootItem(int index)
    {
        inventoryChannel.AddAmountOfItems?.Invoke(containerItems[index].Item, containerItems[index].AmountOfItems, ()=> {
            RemoveItemWithIndex(index);
        });
    }

    private void RemoveItemWithIndex(int index)
    {
        containerItems.RemoveAt(index);
    }

    public void RestoreState(object state)
    {
        var data = (SaveData)state;
        hasBeenOpened = data.hasBeenOpened;

        containerItems.Clear();
        foreach (var item in data.items)
        {
            containerItems.Add(new ContainerSlot
            {
                Item = itemDatabaseChannel.FetchItemFromDatabaseWithID?.Invoke(item),
                Id = UnityEngine.Random.Range(0, 999999999)
            });
        }
        inventoryChannel.OpenedContainerEvent?.Invoke(containerItems, ItemLooting);
    }

    public object CaptureState()
    {
        List<int> ids = new List<int>();

        foreach (var item in containerItems)
        {
            ids.Add(item.Item.ItemId);
        }

        return new SaveData {
            hasBeenOpened = hasBeenOpened,
            items = ids
        };
    }

    [Serializable]
    public struct SaveData
    {
        public bool hasBeenOpened;
        public List<int> items;
    }

}

[Serializable]
public class ContainerSlot
{
    public Item Item;
    public int AmountOfItems;
    public int Id;
}