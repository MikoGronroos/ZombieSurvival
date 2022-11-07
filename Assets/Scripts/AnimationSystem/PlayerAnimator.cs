using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private AnimationChannel animationChannel;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animationChannel.Trigger += Trigger;
        animationChannel.SetFloat += SetFloat;
        animationChannel.SetBool += SetBool;
        animationChannel.SetInt += SetInt;
    }

    private void OnDisable()
    {
        animationChannel.Trigger -= Trigger;
        animationChannel.SetFloat -= SetFloat;
        animationChannel.SetBool -= SetBool;
        animationChannel.SetInt -= SetInt;
    }

    private void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    private void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    private void Trigger(string name)
    {
        animator.SetTrigger(name);
    }

    private void SetInt(string name, int value)
    {
        animator.SetInteger(name, value);
    }

}

public enum WeaponNumber
{
    Unarmed,
    Rifle
}

public enum InteractionNumber
{
    UnarmedPickup,
    UnarmedDoor,
    Axe,
    Pickaxe,
    Shovel,
    Talk
}