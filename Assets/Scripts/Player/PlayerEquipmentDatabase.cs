using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/PlayerEquipmentDatabase")]
public class PlayerEquipmentDatabase : ScriptableObject
{

    [SerializeField] private List<EquippedItem> playerEquipment = new List<EquippedItem>();

    public List<EquippedItem> PlayerEquipment { get { return playerEquipment; } private set { } }

    public EquippedItem FindEquippedItem(Item item)
    {
        foreach (var equipment in playerEquipment)
        {
            if (equipment.CurrentDatabaseItem.Item == item)
            {
                return equipment;
            }
        }
        return null;
    }

    public EquippedItem FindEquippedItemWithType(EquipmentType type)
    {

        foreach (var currentItem in playerEquipment)
        {
            if ((currentItem.CurrentDatabaseItem.Item as ItemEquipment).Type == type)
            {
                return currentItem;
            }
        }

        return null;
    }

}
