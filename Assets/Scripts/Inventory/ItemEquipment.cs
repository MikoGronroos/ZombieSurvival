using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Equipment")]
public abstract class ItemEquipment : Item
{

    [SerializeField] private EquipmentType type;
    [SerializeField] private GameObject equipmentPrefab;

    public EquipmentType Type { get { return type; } set { type = value; } }
    
    public GameObject EquipmentPrefab { get { return equipmentPrefab; } set { equipmentPrefab = value; } }

}
