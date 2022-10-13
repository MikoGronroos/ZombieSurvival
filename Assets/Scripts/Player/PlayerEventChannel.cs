using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/PlayerChannel")]
public class PlayerEventChannel : ScriptableObject
{

    public delegate bool TransformIsVisible(Transform target);
    public delegate Transform GetTransform(Vector3 target, float distance);

    public TransformIsVisible TransformIsVisibleEvent { get; set; }
    public GetTransform GetTransformFromRaycast { get; set; }

    public Action DeadEvent { get; set; }

}