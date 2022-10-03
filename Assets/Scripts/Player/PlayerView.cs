using UnityEngine;

public class PlayerView : MonoBehaviour
{

    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void OnEnable()
    {
        playerEventChannel.TransformIsVisibleEvent += TransformIsVisibleListener;
    }

    private void OnDisable()
    {
        playerEventChannel.TransformIsVisibleEvent -= TransformIsVisibleListener;
    }

    private bool TransformIsVisibleListener(Transform target)
    {
        return Physics.Raycast(raycastOrigin.position, target.position - raycastOrigin.position, Mathf.Infinity);
    }

}
