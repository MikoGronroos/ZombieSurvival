using UnityEngine;

public class ItemPickupSpeedFormula
{

    public const float baseSpeed = 1f;

    public float GetItemPickupSpeed(float weight)
    {
        return baseSpeed * weight;
    }

}
