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

    [SerializeField] private bool automatic;

    [Tooltip("How fast bullets come out from automatic gun")]
    [SerializeField] private float fireRate;

    [Tooltip("Time after you can shoot again")]
    [SerializeField] private float fireDelay;

    private float _timeSinceLastShot = 0;

    private bool _shooting = false;
    private bool _reloading = false;
    private bool _readyToShoot = true;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        AnimMethodChannel.ResetReloadEvent += Reload;
        AnimMethodChannel.ResetAttackEvent += ResetGun;
        AnimMethodChannel.ShootEvent += Shoot;
        inputEventChannel.SwitchFiremode += SwitchFiremode;
    }

    private void OnDisable()
    {
        AnimMethodChannel.ResetReloadEvent -= Reload;
        AnimMethodChannel.ResetAttackEvent -= ResetGun;
        AnimMethodChannel.ShootEvent -= Shoot;
        inputEventChannel.SwitchFiremode -= SwitchFiremode;
    }

    private void Update()
    {
        _timeSinceLastShot = Mathf.Clamp(_timeSinceLastShot += 1 * Time.deltaTime, 0, fireDelay);

        if (automatic)
        {
            _shooting = inputEventChannel.IsHoldingDownAttack;
        }
        else
        {
            _shooting = inputEventChannel.IsAttacking;
        }

        if (inputEventChannel.IsAiming && _readyToShoot && _shooting && !_reloading && CanShoot())
        {
            _readyToShoot = false;
            playerEventChannel.IsAttacking?.Invoke();
        }

    }

    private void Shoot()
    {
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
                        damageable.DoDamage(damage);
                    }
                }
            }
        }

    }

    private void ResetGun()
    {
        _readyToShoot = true;
    }

    private void SwitchFiremode()
    {
        automatic = !automatic;
    }

    private void Reload()
    {
        int amountOfAmmoInInventory = (int)inventoryEventChannel.GetAmountOfItems?.Invoke(neededAmmunition);
        int neededAmmo = maxAmmo - currentAmmo;
        if (amountOfAmmoInInventory < neededAmmo)
        {
            neededAmmo = amountOfAmmoInInventory;
        }
        inventoryEventChannel.RemoveAmountOfItems?.Invoke(neededAmmunition, neededAmmo);
        currentAmmo = currentAmmo + neededAmmo;
    }

    public bool CanReload()
    {
        //Check if inventory has more than 1 bullet so the gun can reload
        if (!(bool)inventoryEventChannel.HasAmountOfItems?.Invoke(neededAmmunition, 1))
        {
            return false;
        }
        return true;
    }

    public bool CanShoot()
    {
        return currentAmmo > 0;
    }

    private bool CheckRange(Transform target)
    {
        return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z)) < range;
    }

}
