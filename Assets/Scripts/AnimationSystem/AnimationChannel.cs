using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/AnimationChannel")]
public class AnimationChannel : ScriptableObject
{

    public delegate void AnimationDelegateString(string name);
    public delegate void AnimationDelegateBool(string name, bool value);
    public delegate void AnimationDelegateFloat(string name, float value);

    public AnimationDelegateString Trigger { get; set; }
    public AnimationDelegateBool SetBool { get; set; }
    public AnimationDelegateFloat SetFloat { get; set; }

}
