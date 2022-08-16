using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Inventory/Item Consumable")]
public class ItemConsumable : Item
{
    [SerializeField] private List<ConsumableValue> consumableValues = new List<ConsumableValue>();

    public List<ConsumableValue> ConsumableValues { get { return consumableValues; } }
}
public enum ConsumableType
{
    Hunger,
    Thrist
}

[Serializable]
public class ConsumableValue
{
    [SerializeField] private ConsumableType consumableType;
    [SerializeField] private float value;

    public ConsumableType ConsumableType { get { return consumableType; } }

    public float Value { get { return value; } }

}