using System;
using UnityEngine;

[Serializable]
public class HealthSystem
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [Tooltip("Makes the npc immortal.")]
    [SerializeField] private bool immortal;

    private Action _healthZeroCallback;

    public float MaxHealth { get { return maxHealth; } private set { } }

    public float CurrentHealth { get { return currentHealth; } private set { } }

    public void SetupHealthSystem(Action healthZeroCallback)
    {
        _healthZeroCallback = healthZeroCallback;
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        if (immortal) return;

        currentHealth = Mathf.Clamp(currentHealth -= amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            _healthZeroCallback?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth += amount, 0, maxHealth);
    }

}
