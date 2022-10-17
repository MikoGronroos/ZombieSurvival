using UnityEngine;

public class AnimationMethods : MonoBehaviour
{

    public void EndMeleeAttack()
    {
        AnimMethodChannel.EndMeleeAttack?.Invoke();
    }

}
