using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private ItemWeapon weapon;

    private Collider weaponCollider;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public void ToggleCollider(bool value)
    {

        if (weaponCollider == null) return;

        weaponCollider.enabled = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(weapon.Damage);
        }
    }

}
