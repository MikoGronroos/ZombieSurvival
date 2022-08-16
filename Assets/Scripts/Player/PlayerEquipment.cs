using System;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerEquipment : MonoBehaviour
{

    [SerializeField] private PlayerEquipmentDatabase playerEquipment;

    [SerializeField] private List<GameObject> playerInstancedEquipmentPrefabs = new List<GameObject>();

    [SerializeField] private Transform handGameObjectParent;

    [SerializeField] private InventoryChannel inventoryChannel;

    private void OnEnable()
    {
        inventoryChannel.InventoryEquip += EquipListener;
        inventoryChannel.InventoryDequip += DequipListener;
    }

    private void OnDisable()
    {
        inventoryChannel.InventoryEquip -= EquipListener;
        inventoryChannel.InventoryDequip -= DequipListener;
    }

    private void Start()
    {
        List<EquippedItem> items = new List<EquippedItem>(playerEquipment.PlayerEquipment);
        foreach (var item in items)
        {
            EquipItem(item.CurrentDatabaseItem);
        }
    }

    private void DequipListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var item = inventoryChannel.FetchInventoryItemWithIndex?.Invoke(args);
        DequipItem(item);
    }

    private void EquipListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var item = inventoryChannel.FetchInventoryItemWithIndex?.Invoke(args);
        EquipItem(item);
    }

    public void EquipItem(DatabaseItem item)
    {
        var newItem = item.Item as ItemEquipment;
        EquippedItem equippedItem = new EquippedItem();
        equippedItem.CurrentDatabaseItem = item;
        if (HasTypeEquipped(newItem.Type))
        {
            DequipItem(playerEquipment.FindEquippedItemWithType(newItem.Type).CurrentDatabaseItem);
        }
        GameObject obj = Instantiate(newItem.EquipmentPrefab, GetEquipmentParenTransform(newItem.Type));
        obj.transform.rotation = Quaternion.identity;
        playerInstancedEquipmentPrefabs.Add(obj);
        equippedItem.CurrentEquipmentPrefabIndex = playerInstancedEquipmentPrefabs.IndexOf(obj);
        playerEquipment.PlayerEquipment.Add(equippedItem);
        item.Equipped = true;
    }

    public void DequipItem(DatabaseItem item)
    {
        if (item.Equipped)
        {
            var equippedItem = playerEquipment.FindEquippedItem(item.Item);
            playerEquipment.PlayerEquipment.Remove(equippedItem);
            if (playerInstancedEquipmentPrefabs.Count > 0)
            {
                GameObject temp = playerInstancedEquipmentPrefabs[equippedItem.CurrentEquipmentPrefabIndex];
                playerInstancedEquipmentPrefabs.RemoveAt(equippedItem.CurrentEquipmentPrefabIndex);
                Destroy(temp);
            }
            item.Equipped = false;
        }
    }

    private bool HasTypeEquipped(EquipmentType type)
    {

        if (playerEquipment.PlayerEquipment.Count <= 0) return false;

        foreach (var equipment in playerEquipment.PlayerEquipment)
        {
            if((equipment.CurrentDatabaseItem.Item as ItemEquipment).Type == type)
            {
                return true;
            }
        }
        return false;
    }

    private Transform GetEquipmentParenTransform(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Head:

                break;
            case EquipmentType.Torso:

                break;
            case EquipmentType.Legs:

                break;
            case EquipmentType.Feet:

                break;
            case EquipmentType.Hand:
                return handGameObjectParent;
        }
        return null;
    }

}

public enum EquipmentType
{
    Head,
    Torso,
    Legs,
    Feet,
    Hand
}

[Serializable]
public class EquippedItem
{
    [SerializeField] private DatabaseItem currentDatabaseItem;
    [SerializeField] private int currentEquipmentPrefabIndex;

    public DatabaseItem CurrentDatabaseItem { get { return currentDatabaseItem; } set { currentDatabaseItem = value; } }

    public int CurrentEquipmentPrefabIndex { get { return currentEquipmentPrefabIndex; } set { currentEquipmentPrefabIndex = value; } }
}
