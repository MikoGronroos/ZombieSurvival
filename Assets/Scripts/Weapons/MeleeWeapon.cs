using UnityEngine;

public class MeleeWeapon : Weapon
{

    private Collider weaponCollider;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public override void Attack()
    {
        if (weaponCollider == null) return;

        weaponCollider.enabled = true;
    }

    public override void EndAttack()
    {
        if (weaponCollider == null) return;

        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(weapon.Damage);
        }
    }

}