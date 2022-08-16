using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{

    [SerializeField] private HealthSystem healthSystem = new HealthSystem();

    private void Start()
    {
        healthSystem.SetupHealthSystem(null);
    }

    public void DoDamage(float damage)
    {
        healthSystem.Damage(damage);
    }
}
