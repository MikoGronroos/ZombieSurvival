using UnityEngine;
using System.Collections.Generic;
using Finark.AI;
using System;

public class NPCZombie : NPCBase
{

    [SerializeField] private Transform target;

    public override void Start()
    {
        base.Start();

        combatSystem.SetTarget(target);

        MoveState moveState = new MoveState(this, target);
        AttackState attackState = new AttackState(combatSystem.AttackSpeed, this);

        AddAnyTransition(attackState, new List<Func<bool>> { combatSystem.IsInRange });
        AddAnyTransition(moveState, new List<Func<bool>> { combatSystem.IsNotInRange });

        SwitchState(moveState);

    }

    public override void OnHealthHitZero()
    {
        Destroy(gameObject);
    }

}

public class MoveState : State
{

    private NPCBase _npcBase;
    private Transform _target;

    public MoveState(NPCBase npcBase, Transform target)
    {
        _npcBase = npcBase;
        _target = target;
    }

    public override void EnterState(StateMachine machine)
    {
        _npcBase.NavigationSystem.ToggleAgent(true);
        _npcBase.AnimationSystem.PlayAnimation("Unarmed-Walk-Injured");
    }

    public override void ExitState(StateMachine machine)
    {
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
        if (!_npcBase.CanMove) return;

        _npcBase.NavigationSystem.MoveToPosition(_target.position);
    }
}

public class AttackState : State
{

    private Timer _timer = null;
    private float _attackSpeed;
    private NPCBase _npcBase;

    public AttackState(float attackSpeed, NPCBase npcBase)
    {
        _attackSpeed = attackSpeed;
        _npcBase = npcBase;
    }

    public override void EnterState(StateMachine machine)
    {
        _npcBase.AnimationSystem.PlayAnimation("Unarmed-Run-Forward-Attack1-Right");
        _timer = new Timer(_attackSpeed, Attack);
    }

    public override void ExitState(StateMachine machine)
    {
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
        _timer.Tick();
    }

    private void Attack()
    {
        _timer.ResetTimer();
    }

}