using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWeightSystem
{

    [Tooltip("Max carry weight in kilograms.")]
    [SerializeField] private float maxCarryWeight;
    [SerializeField] private float currentCarryWeight;

    public float MaxCarryWeight { get { return maxCarryWeight; } }

    public float CurrentCarryWeight { get { return currentCarryWeight; } }

    public void AddWeight(float weight)
    {
        currentCarryWeight += weight;
    }

    public void RemoveWeight(float weight)
    {
        currentCarryWeight -= weight;
    }

    public void ChangePlayerWeight(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var weight = (float)args["Weight"];
        if (weight > 0)
        {
            AddWeight(weight);
        }
        else
        {
            RemoveWeight(weight);
        }
    }

}