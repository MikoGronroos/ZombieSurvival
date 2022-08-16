using System;
using UnityEngine;

[Serializable]
public class CombatSystem
{

    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float attackSpeed;

    [SerializeField] private Transform target;

    private Transform _agentTransform;

    public float AttackSpeed { get { return attackSpeed; } }

    public float Range { get { return range; } }

    public float Damage { get { return damage; } }

    public void SetupCombatSystem(Transform agentTransform)
    {
        _agentTransform = agentTransform;
    }

    public bool IsInRange()
    {
        return Vector3.Distance(_agentTransform.position, target.position) < range;
    }

    public bool IsNotInRange()
    {
        return Vector3.Distance(_agentTransform.position, target.position) > range;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}
