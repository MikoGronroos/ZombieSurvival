using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Equipment")]
public class ItemEquipment : Item
{

    [SerializeField] private EquipmentType type;
    [SerializeField] private GameObject equipmentPrefab;

    public EquipmentType Type { get { return type; } private set { } }
    
    public GameObject EquipmentPrefab { get { return equipmentPrefab; } private set { } }

}
