using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class NavigationSystem
{

    [SerializeField] private float speed;

    private NavMeshAgent _agent;

    public void SetupNavigationSystem(NavMeshAgent agent)
    {
        _agent = agent;
        _agent.speed = speed;
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void ToggleAgent(bool value)
    {
        _agent.enabled = value;
    }

}
