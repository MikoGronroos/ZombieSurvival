using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Database")]
public class InventoryDatabase : ScriptableObject
{

    [SerializeField] private int databaseMaxSize = 0;

    [SerializeField] private List<DatabaseItem> database = new List<DatabaseItem>();

    public int DatabaseMaxSize { get { return databaseMaxSize; } private set { } }

    public List<DatabaseItem> Database { get { return database; } private set { } }
}

[System.Serializable]
public class DatabaseItem
{

    [SerializeField] private Item item;
    [SerializeField] private int currentStackSize = 0;
    [SerializeField] private bool equipped;
    [SerializeField] private int slotId;

    public Item Item { get { return item; } private set { } }
    public int CurrentStackSize { get { return currentStackSize; } private set { } }
    public bool Equipped { get { return equipped; } set { equipped = value; } }
    public int SlotId { get { return slotId; } set { slotId = value; } }

    public DatabaseItem(Item item)
    {
        this.item = item;
        slotId = Random.Range(0,999999999);
        IncrementStack();
    }

    public void IncrementStack()
    {
        currentStackSize++;
    }

    public void DecrementStack()
    {
        currentStackSize--;
    }

    public bool HasSpaceOnStack()
    {
        return currentStackSize < item.MaxStackSize;
    }

    public bool IsLastItemOnStack()
    {
        return currentStackSize <= 1;
    }

}