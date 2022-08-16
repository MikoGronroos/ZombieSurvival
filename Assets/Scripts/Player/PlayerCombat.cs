using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private PlayerCombatChannel playerCombatChannel;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (InputSystem.Instance.IsAttacking)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100))
            {
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.DoDamage((float)playerCombatChannel.GetPlayerDamage?.Invoke(null));
                }
            }
        }
    }
}
