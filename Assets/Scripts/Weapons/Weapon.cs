using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected ItemWeapon weapon;
    [SerializeField] protected PlayerSkill weaponSkill;

    [SerializeField] protected float damage;
    [SerializeField] protected float range;

    [Header("Animations")]
    [SerializeField] protected string weaponAimAnimation;

    [Header("Hand Positions")]
    [SerializeField] private Transform rightHandPos;
    [SerializeField] private Transform leftHandPos;

    public Transform RightHandPos { get { return rightHandPos; } }

    public Transform LeftHandPos { get { return leftHandPos; } }

    [Header("References")]
    [SerializeField] protected InputEventChannel inputEventChannel;

    public virtual void Attack() { }

    public virtual void EndAttack() { }

    public string GetWeaponAimAnimation()
    {
        return weaponAimAnimation;
    }

}
