using UnityEngine;

public class CombatCollider : MonoBehaviour
{

    [SerializeField] private Collider combatCollider;

    [SerializeField] private HandleCollision handleCollision;

    private NPCBase _npcBase;

    private void OnEnable()
    {
        handleCollision.TriggerEnterEvent += HandleCollision;
    }

    private void OnDisable()
    {
        handleCollision.TriggerEnterEvent -= HandleCollision;
    }

    private void Awake()
    {
        _npcBase = GetComponent<NPCBase>();
    }

    private void HandleCollision(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerStatsManager playerStatsManager))
        {
            playerStatsManager.CurrentPlayerStats.HealthSystem.Damage(_npcBase.CombatSystem.Damage);
        }
    }

    public void ToggleCombatCollider(int value)
    {
        combatCollider.enabled = value == 0 ? false : true;
    }

}
