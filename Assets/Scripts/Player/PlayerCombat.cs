using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private PlayerStatsChannel playerStatsChannel;

    [SerializeField] private InteractionData interactionData;

    private Animator _playerAnimator;

    private Camera _cam;

    private bool _canAttack;

    private void Awake()
    {
        _cam = Camera.main;
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _canAttack = true;
    }

    private void Update()
    {
        if (InputSystem.Instance.IsAttacking && _canAttack)
        {
            _canAttack = false;

            _playerAnimator.SetBool("isAttacking", true);
        }
    }

    public void ResetAttack()
    {
        _playerAnimator.SetBool("isAttacking", false);
        _canAttack = true;
    }

}
