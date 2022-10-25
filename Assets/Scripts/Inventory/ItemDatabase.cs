using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemDatabase", fileName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{

    [SerializeField] private List<Item> items = new List<Item>();

    private Dictionary<string, Item> database = new Dictionary<string, Item>();

    public Dictionary<string, Item> Database { get { return database; } private set { } }

    public void Setup()
    {
        foreach (var item in items)
        {
            database.Add(item.ItemId, item);
        }
    }
}
