using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseManager : MonoBehaviour
{

    [SerializeField] private ItemDatabase currentItemDatabase;

    [SerializeField] private ItemDatabaseChannel itemDatabaseChannel;

    private void Awake()
    {
        currentItemDatabase.Setup();
    }

    private void OnEnable()
    {
        itemDatabaseChannel.FetchItemFromDatabaseWithID += FetchItemWithID;
    }

    private void OnDisable()
    {
        itemDatabaseChannel.FetchItemFromDatabaseWithID -= FetchItemWithID;
    }

    private Item FetchItemWithID(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        int id = (int)args["id"];
        if (currentItemDatabase.Database.ContainsKey(id))
        {
            return currentItemDatabase.Database[id];
        }
        return null;
    }

}
