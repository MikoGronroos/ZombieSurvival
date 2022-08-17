using UnityEngine;

public abstract class Item : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int itemId;
    [SerializeField] private int maxStackSize;

    [SerializeField] private float weight;

    public int MaxStackSize { get { return maxStackSize; } private set { } }

    public float Weight { get { return weight; } private set { } }

    public Sprite ItemIcon { get { return itemIcon; } private set { } }

    public int ItemId { get { return itemId; } private set { } }

    public string ItemName { get { return itemName; } private set { } }

}
