using UnityEngine;

[System.Serializable]
public class PlayerVitalsSystem
{

    #region Variables

    [SerializeField] private float maxHydration;
    [SerializeField] private float currentHydration;

    [SerializeField] private float maxFullness;
    [SerializeField] private float currentFullness;

    public float CurrentHydration { get { return currentHydration; } private set { } }

    public float MaxHydration { get { return maxHydration; } private set { } }

    public float MaxFullness { get { return maxFullness; } private set { } }

    public float CurrentFullness { get { return currentFullness; } private set { } }

    #endregion

    public void AddFullness(float amount)
    {
        currentFullness = Mathf.Clamp(currentFullness + amount, 0, maxFullness);
    }

    public void AddHydration(float amount)
    {
        currentHydration = Mathf.Clamp(currentHydration + amount, 0, maxHydration);
    }

}