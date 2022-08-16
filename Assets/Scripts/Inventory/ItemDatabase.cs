using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemDatabase", fileName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{

    [SerializeField] private List<Item> items = new List<Item>();

    private Dictionary<int, Item> database = new Dictionary<int, Item>();

    public Dictionary<int, Item> Database { get { return database; } private set { } }

    public void Setup()
    {
        foreach (var item in items)
        {
            database.Add(item.ItemId, item);
        }
    }
}
