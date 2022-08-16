using UnityEngine;
using System.Collections.Generic;

public class InventorySlotClicked : MonoBehaviour
{

    [SerializeField] private int index;
    [SerializeField] private InventoryChannel inventoryChannel;

    public int Index { get { return index; } set { index = value; } }

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

    }

    public void LootAll()
    {

    }

}