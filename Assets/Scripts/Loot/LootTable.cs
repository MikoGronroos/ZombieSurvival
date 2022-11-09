using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/LootTable")]
public class LootTable : ScriptableObject
{

    [SerializeField] private ContainerType containerType;
    [SerializeField] private LootTableItem[] lootTable;

    public LootTableItem[] CurrentLootTable { get { return lootTable; } private set { } }

    [System.Serializable]
    public class LootTableItem
    {
        public Item Item;

        [Range(0,1)]
        public float ChanceOfSpawning;

        public int MinAmountToSpawn;
        public int MaxAmountToSpawn;

    }

    public List<ContainerSlot> GetLoot()
    {
        List<ContainerSlot> slots = new List<ContainerSlot>();

        foreach (var item in lootTable)
        {
            float diceRoll = Random.Range(0, 100);
            if (diceRoll <= item.ChanceOfSpawning)
            {
                ContainerSlot slot = new ContainerSlot();
                slot.Item = item.Item;
                slot.AmountOfItems = Random.Range(item.MinAmountToSpawn, item.MaxAmountToSpawn);
                slot.Id = Random.Range(0, 999999999);
                slots.Add(slot);
            }

        }
        return slots;
    }

    public void SetValue(int index, float value)
    {
        lootTable[index].ChanceOfSpawning = value;
    }

}