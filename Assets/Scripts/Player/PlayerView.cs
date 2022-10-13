using UnityEditor;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    [SerializeField] private Transform raycastOrigin;

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
        Physics.Raycast(raycastOrigin.position, target - raycastOrigin.position, out raycastHit, distance);
        return raycastHit.transform;
    }

}
