using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [SerializeField] private InteractionData interactionData;

    private void OnEnable()
    {
        interactionData.CanInteractEvent += IsVisibleTarget;
    }

    private void OnDisable()
    {
        interactionData.CanInteractEvent -= IsVisibleTarget;
    }

    private void FindVisibleTargets()
    {

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (var collider in targetsInViewRadius)
        {
            Vector3 dirToTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, collider.transform.position);

                if (Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                }
            }
        }
    }

    private bool IsVisibleTarget(Transform target)
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        {
            float dstToTarget = Vector3.Distance(transform.position, target.position);

            if (dstToTarget > viewRadius) return false;

            if (Physics.Raycast(transform.position, dirToTarget, dstToTarget, targetMask))
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
