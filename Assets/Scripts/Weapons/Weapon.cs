using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected ItemEquipment weapon;
    [SerializeField] protected PlayerSkill weaponSkill;

    [SerializeField] protected float damage;
    [SerializeField] protected float range;

    [Header("Animations")]
    [SerializeField] protected string weaponAttackAnimation;

    [Header("Hand Positions")]
    [SerializeField] private Transform rightHandPos;
    [SerializeField] private Transform leftHandPos;

    [Header("Animation")]
    [SerializeField] private WeaponNumber weaponNumber;

    public Transform RightHandPos { get { return rightHandPos; } }

    public Transform LeftHandPos { get { return leftHandPos; } }

    [Header("References")]
    [SerializeField] protected InputEventChannel inputEventChannel;

    public string GetWeaponAttackAnimation()
    {
        return weaponAttackAnimation;
    }

    public int GetWeaponNumber()
    {
        return (int)weaponNumber;
    }

}

public enum WeaponNumber
{
    Unarmed,
    Rifle
}