using UnityEngine;
using System.Linq;

public class PlayerView : MonoBehaviour
{

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float checkRadius;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void OnEnable()
    {
        playerEventChannel.TransformIsVisibleEvent += TransformIsVisibleListener;
        playerEventChannel.GetTransformFromRaycast += GetTheFirstTransformFromRaycast;
    }

    private void OnDisable()
    {
        playerEventChannel.TransformIsVisibleEvent -= TransformIsVisibleListener;
        playerEventChannel.GetTransformFromRaycast -= GetTheFirstTransformFromRaycast;
    }

    private bool TransformIsVisibleListener(Transform target)
    {
        return Physics.Raycast(raycastOrigin.position, target.position - raycastOrigin.position, Mathf.Infinity);
    }

    private Transform GetTheFirstTransformFromRaycast(Vector3 target, float distance)
    {
        RaycastHit raycastHit;
        target.y = raycastOrigin.position.y / 2;
        Physics.Raycast(raycastOrigin.position, target - raycastOrigin.position, out raycastHit, distance);
        Debug.DrawRay(raycastOrigin.position, (target - raycastOrigin.position) * distance, Color.cyan);
        return raycastHit.transform;
    }

}
