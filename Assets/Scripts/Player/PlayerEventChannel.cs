using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/PlayerChannel")]
public class PlayerEventChannel : ScriptableObject
{

    public delegate bool TransformIsVisible(Transform target);

    public TransformIsVisible TransformIsVisibleEvent { get; set; }

}