using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected ItemWeapon weapon;
    [SerializeField] protected PlayerSkill weaponSkill;

    [Header("Animations")]
    [SerializeField] protected string weaponAttackAnimation;
    [SerializeField] protected string weaponReloadAnimation;

    [Header("References")]
    [SerializeField] protected InputEventChannel inputEventChannel;

    public virtual void Attack() { }

    public virtual void EndAttack() { }

    public string GetWeaponAttackAnimation()
    {
        return weaponAttackAnimation;
    }

    public string GetWeaponReloadAnimation()
    {
        return weaponReloadAnimation;
    }

}
