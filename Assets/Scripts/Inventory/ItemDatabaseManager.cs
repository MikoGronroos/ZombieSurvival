using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseManager : MonoBehaviour
{

    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    private Dictionary<string, Item> database = new Dictionary<string, Item>();

    public Dictionary<string, Item> Database { get { return database; } private set { } }

    private void OnEnable()
    {
        itemDatabaseChannel.FetchItemFromDatabaseWithID += FetchItemWithID;
    }

    private void OnDisable()
    {
        itemDatabaseChannel.FetchItemFromDatabaseWithID -= FetchItemWithID;
    }

    private void Awake()
    {
        Item[] items = Resources.LoadAll<Item>("Items/");

        foreach (Item item in items)
        {
            if (!database.ContainsKey(item.ItemId))
            {
                database.Add(item.ItemId, item);
            }
        }
    }

    private Item FetchItemWithID(string id)
    {
        if (database.ContainsKey(id))
        {
            return database[id];
        }
        return null;
    }

}
