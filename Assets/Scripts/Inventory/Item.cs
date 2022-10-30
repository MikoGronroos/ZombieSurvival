using System;
using UnityEngine;

public abstract class Item : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemId;
    [SerializeField] private int maxStackSize;

    [SerializeField] private float weight;

    public int MaxStackSize { get { return maxStackSize; } set { maxStackSize = value; } }

    public float Weight { get { return weight; } set { weight = value; } }

    public Sprite ItemIcon { get { return itemIcon; } set { itemIcon = value; } }

    public string ItemId { get { return itemId; } set { itemId = value; } }

    public string ItemName { get { return itemName; } set { itemName = value; } }

    public void GenerateId() => itemId = Guid.NewGuid().ToString();

}
