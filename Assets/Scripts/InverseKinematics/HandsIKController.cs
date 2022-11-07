using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HandsIKController : MonoBehaviour
{

    [SerializeField] private RigBuilder rigBuilder;

    [Header("Hand transforms")]
    [SerializeField] private TwoBoneIKConstraint leftHand;
    [SerializeField] private TwoBoneIKConstraint rightHand;

    [SerializeField] private WeaponChannel weaponChannel;

    private void OnEnable()
    {
        weaponChannel.SwitchWeaponEvent += SetIKPositions;
        weaponChannel.ResetIKPositions += ResetIKPositions;
    }

    private void OnDisable()
    {
        weaponChannel.SwitchWeaponEvent -= SetIKPositions;
        weaponChannel.ResetIKPositions -= ResetIKPositions;
    }

    private void SetIKPositions(Weapon weapon)
    {
        if (weapon.LeftHandPos != null)
        {
            leftHand.data.target = weapon.LeftHandPos;
        }
        if (weapon.RightHandPos != null)
        {
            rightHand.data.target = weapon.RightHandPos;
        }
        rigBuilder.Build();
    }

    private void ResetIKPositions()
    {
        leftHand.data.target = null;
        rightHand.data.target = null;
        rigBuilder.Build();
    }

}
