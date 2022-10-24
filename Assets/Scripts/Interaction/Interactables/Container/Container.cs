using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable, ISaveable
{

    [SerializeField] private string containerName;

    [SerializeField] private float interactionTime;

    [SerializeField] private bool hasBeenOpened;

    [SerializeField] private List<ContainerSlot> containerItems = new List<ContainerSlot>();

    [SerializeField] private InventoryChannel inventoryChannel;
    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    [SerializeField] private LootTable lootTable;

    private bool _hasBeenInitialized = false;

    private void Start()
    {
        if (!_hasBeenInitialized)
        {
            containerItems = lootTable.GetLoot();
            _hasBeenInitialized = true;
        }
    }

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
        for (int i = 0; i < data.items.Count; i++)
        {
            containerItems.Add(new ContainerSlot
            {
                Item = itemDatabaseChannel.FetchItemFromDatabaseWithID?.Invoke(data.items[i]),
                AmountOfItems = data.itemAmounts[i],
                Id = UnityEngine.Random.Range(0, 999999999)
            });
        }
    }

    public object CaptureState()
    {
        List<int> ids = new List<int>();
        List<int> amounts = new List<int>();

        foreach (var item in containerItems)
        {
            ids.Add(item.Item.ItemId);
            amounts.Add(item.AmountOfItems);
        }

        return new SaveData
        {
            hasBeenOpened = hasBeenOpened,
            items = ids,
            itemAmounts = amounts
        };

    }

    [Serializable]
    public struct SaveData
    {
        public bool hasBeenOpened;
        public List<int> items;
        public List<int> itemAmounts;
    }

}

[Serializable]
public class ContainerSlot
{
    public Item Item;
    public int AmountOfItems;
    public int Id;
}

public enum ContainerType
{
    LightMilitaryContainer,
    HeavyMilitaryContainer,
    MedicalMilitaryContainer
}