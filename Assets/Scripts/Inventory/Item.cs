using System;
using UnityEngine;

public abstract class Item : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemId;
    [SerializeField] private int maxStackSize;

    [SerializeField] private float weight;

    public int MaxStackSize { get { return maxStackSize; } private set { } }

    public float Weight { get { return weight; } private set { } }

    public Sprite ItemIcon { get { return itemIcon; } private set { } }

    public string ItemId { get { return itemId; } private set { } }

    public string ItemName { get { return itemName; } private set { } }

    public void GenerateId() => itemId = Guid.NewGuid().ToString();

}
