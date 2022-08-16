using UnityEngine;

[System.Serializable]
public class ItemClothing : ItemEquipment
{

    [SerializeField] private int armorRating;
    [SerializeField] private ClothingState clothingState;

}

public enum ClothingState
{
    Dry,
    Wet,
    Soaked
}
