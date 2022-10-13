using System;
using System.Collections;
using UnityEngine;

public class RangedWeapon : Weapon
{

    [Header("Ranged Weapon")]

    [SerializeField] private LayerMask hitMask;
    [SerializeField] private PlayerEventChannel playerEventChannel;
    [SerializeField] private InventoryChannel inventoryEventChannel;
    [SerializeField] private PlayerSkillEventChannel playerSkillEventChannel;

    [SerializeField] private Transform muzzleFlashParent;

    [SerializeField] private Item neededAmmunition;

    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxAmmo;

    [SerializeField] private bool automatic;

    [SerializeField] private float fireRate;

    [Tooltip("better quality weapons have more baseHitChance")]
    [Range(0,90)]
    [SerializeField] private int baseHitChance;

    [Header("Recoil")]
    [SerializeField] private float recoilPower;
    [SerializeField] private AnimationCurve recoilEffectToAccuracy;

    [SerializeField] private float recoilResetTime = 0f;

    [Header("GFX")]
    [SerializeField] private GameObject[] muzzleFlashes;

    private bool _shooting = false;
    private bool _reloading = false;
    private bool _readyToShoot = true;
    private int _currentShotsInRow;
    private Coroutine _recoil;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        inputEventChannel.SwitchFiremode += SwitchFiremode;
    }

    private void OnDisable()
    {
        inputEventChannel.SwitchFiremode -= SwitchFiremode;
    }

    private void Update()
    {
        if (automatic)
        {
            _shooting = inputEventChannel.IsHoldingDownAttack;
        }
        else
        {
            _shooting = inputEventChannel.IsAttacking;
        }

        if (inputEventChannel.IsAiming && _readyToShoot && _shooting && !_reloading && currentAmmo > 0)
        {
            _readyToShoot = false;
            Shoot();
        }
    }

    private IEnumerator LerpRecoil(float start, float end, float lerpTime = 1)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpTime)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.Clamp(Mathf.Lerp(start, end, timeElapsed / lerpTime), 0, 360), transform.localEulerAngles.z);
            timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, lerpTime);
            yield return null;
        }
        _currentShotsInRow = 0;
    }

    private void Shoot()
    {

        Recoil();
        MuzzleFlash();

        currentAmmo--;
        _currentShotsInRow = Mathf.Clamp(_currentShotsInRow + 1, 0, 10);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (CheckRange(hit.transform))
            {
                if ((bool)playerEventChannel.TransformIsVisibleEvent?.Invoke(hit.transform))
                {
                    if (hit.transform.TryGetComponent(out IDamageable damageable))
                    {
                        bool hitted = false;
                        float hitChance = Mathf.Clamp(baseHitChance - (1 * GetRecoilEffectOnAccuracy() * 10) + weaponSkill.Level * 5, 0, 90);
                        Debug.Log(hitChance);
                        float randomNumber = UnityEngine.Random.Range(0, 100);
                        if (randomNumber <= hitChance) 
                        { 
                            hitted = true;
                        }
                        if (hitted)
                        {
                            damageable.DoDamage(damage);
                        }
                        playerSkillEventChannel.ProgressSkillEvent?.Invoke(weaponSkill);
                    }
                }
            }
        }

        Invoke("ResetShot", fireRate);

    }

    private void MuzzleFlash()
    {
        Instantiate(muzzleFlashes[UnityEngine.Random.Range(0, muzzleFlashes.Length - 1)], muzzleFlashParent);
    }

    private void Recoil()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y - recoilPower, 0);
        if (_recoil != null)
        {
            StopCoroutine(_recoil);
        }
        _recoil = StartCoroutine(LerpRecoil(transform.localEulerAngles.y, 360, recoilResetTime));
    }

    private void ResetShot()
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

    private float GetRecoilEffectOnAccuracy()
    {
        return recoilEffectToAccuracy.Evaluate((float)_currentShotsInRow / 10);
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

    private bool CheckRange(Transform target)
    {
        return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z)) < range;
    }

}
