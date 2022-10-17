using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Weapon currentWeapon;

    [SerializeField] private WeaponChannel weaponChannel;

    public Weapon CurrentWeapon { get { return currentWeapon; } }

    private void OnEnable()
    {
        weaponChannel.SwitchWeaponEvent += SwitchWeaponListener;
    }

    private void OnDisable()
    {
        weaponChannel.SwitchWeaponEvent -= SwitchWeaponListener;
    }

    private void SwitchWeaponListener(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public void EnableWeaponCollider()
    {

        if (currentWeapon == null) return;

        (currentWeapon as MeleeWeapon).EnableWeaponCollider();
    }

    public void DisableWeaponCollider()
    {

        if (currentWeapon == null) return;

        (currentWeapon as MeleeWeapon).DisableWeaponCollider();
    }

}
