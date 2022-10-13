using UnityEngine;

public class AnimationMethods : MonoBehaviour
{

    public void ShootEvent()
    {
        AnimMethodChannel.ShootEvent?.Invoke();
    }

}
