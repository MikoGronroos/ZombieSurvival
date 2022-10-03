using System;
using UnityEngine;

public class RangedWeapon : Weapon
{

    [SerializeField] private LayerMask hitMask;
    [SerializeField] private PlayerEventChannel playerEventChannel;
    [SerializeField] private InventoryChannel inventoryEventChannel;

    [SerializeField] private Item neededAmmunition;

    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxAmmo;

    private void Start()
    {
        Reload();
    }

    private void OnEnable()
    {
        AnimMethodChannel.ResetReloadEvent += Reload;
    }

    private void OnDisable()
    {
        AnimMethodChannel.ResetReloadEvent -= Reload;
    }

    public override void Attack()
    {

        if (currentAmmo <= 0) return;

        currentAmmo--;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (CheckRange(hit.transform))
            {
                if ((bool)playerEventChannel.TransformIsVisibleEvent?.Invoke(hit.transform))
                {
                    if (hit.transform.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.DoDamage(weapon.Damage);
                    }
                }
            }
        }
    }

    public override void EndAttack()
    {
    }

    private void Reload()
    {
        int neededAmmo = maxAmmo - currentAmmo;
        inventoryEventChannel.RemoveAmountOfItems?.Invoke(neededAmmunition, neededAmmo);
        currentAmmo = maxAmmo;
    }

    public bool CanReload()
    {

        if (!(bool)inventoryEventChannel.HasAmountOfItems?.Invoke(neededAmmunition, maxAmmo))
        {
            return false;
        }

        return true;
    }

    private bool CheckRange(Transform target)
    {
        return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z)) < weapon.Range;
    }

}
