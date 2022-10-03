using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMethods : MonoBehaviour
{

    public void ResetAttack()
    {
        AnimMethodChannel.ResetAttackEvent?.Invoke();
    }

    public void ResetReload()
    {
        AnimMethodChannel.ResetReloadEvent?.Invoke();
    }

}
