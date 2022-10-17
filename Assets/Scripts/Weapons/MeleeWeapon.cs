using UnityEngine;

public class MeleeWeapon : Weapon
{

    private Collider weaponCollider;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public void EnableWeaponCollider()
    {
        if (weaponCollider == null) return;

        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        if (weaponCollider == null) return;

        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(damage);
        }
    }

}