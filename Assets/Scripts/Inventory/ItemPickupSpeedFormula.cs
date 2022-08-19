using UnityEngine;

public class ItemPickupSpeedFormula
{

    private const float baseSpeed = 1f;

    public static float GetItemPickupSpeed(float weight)
    {
        return baseSpeed * weight;
    }

}
