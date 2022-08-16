using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Weapon")]
public class ItemWeapon : ItemTool
{

    [SerializeField] private float damage;
    [SerializeField] private float range;

    public float Damage { get { return damage; } private set { } }

    public float Range { get { return range; } private set { } }

}
