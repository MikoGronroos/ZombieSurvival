using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/WeaponChannel")]
public class WeaponChannel : ScriptableObject
{

    public delegate void SwitchWeapon(Weapon weapon);

    public SwitchWeapon SwitchWeaponEvent { get; set; }
    public Action ResetIKPositions { get; set; }

}
