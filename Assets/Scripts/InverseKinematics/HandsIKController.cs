using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsIKController : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [Header("Hand transforms")]
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [Header("IK Positions")]
    [SerializeField] private Transform leftHandIKPosition;
    [SerializeField] private Transform rightHandIKPosition;

    [Header("IK Settings")]
    [SerializeField] private bool ikActive;

    private void OnAnimatorIK()
    {
        if (animator)
        {
            if (ikActive)
            {
                if (leftHandIKPosition != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKPosition.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKPosition.rotation);
                }
                if (rightHandIKPosition != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKPosition.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKPosition.rotation);
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }

}
