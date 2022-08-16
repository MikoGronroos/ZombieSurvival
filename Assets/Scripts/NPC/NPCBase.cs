using UnityEngine;
using UnityEngine.AI;
using Finark.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NPCBase : StateMachine, IDamageable
{

    [SerializeField] protected HealthSystem healthSystem = new HealthSystem();
    [SerializeField] protected NavigationSystem navigationSystem = new NavigationSystem();
    [SerializeField] protected CombatSystem combatSystem = new CombatSystem();
    [SerializeField] protected AnimationSystem animationSystem = new AnimationSystem();

    [SerializeField] private bool canMove;

    public HealthSystem HealthSystem { get { return healthSystem; } }

    public NavigationSystem NavigationSystem { get { return navigationSystem; } }

    public AnimationSystem AnimationSystem { get { return animationSystem; } }

    public CombatSystem CombatSystem { get { return combatSystem; } }

    public override void Start()
    {
        healthSystem.SetupHealthSystem(OnHealthHitZero);
        combatSystem.SetupCombatSystem(transform);
        animationSystem.SetupAnimationSystem(GetComponent<Animator>());
        if (canMove)
        {
            navigationSystem.SetupNavigationSystem(GetComponent<NavMeshAgent>());
        }
    }

    public void DoDamage(float damage)
    {
        healthSystem.Damage(damage);
    }

    public virtual void OnHealthHitZero() { }

}