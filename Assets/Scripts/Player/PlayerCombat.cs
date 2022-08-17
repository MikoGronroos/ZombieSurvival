using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private PlayerStatsChannel playerStatsChannel;

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
                    damageable.DoDamage((float)playerStatsChannel.GetPlayerDamage?.Invoke(null));
                }
            }
        }
    }
}
