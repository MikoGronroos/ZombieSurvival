using UnityEngine;

public abstract class Item : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int itemId;
    [SerializeField] private int maxStackSize;

    public int MaxStackSize { get { return maxStackSize; } private set { } }

    public Sprite ItemIcon { get { return itemIcon; } private set { } }

    public int ItemId { get { return itemId; } private set { } }

    public string ItemName { get { return itemName; } private set { } }

}
