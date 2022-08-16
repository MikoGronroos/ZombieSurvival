using UnityEngine;

[System.Serializable]
public class AnimationSystem
{

    private Animator _animator;
    private string _currentlyPlayingAnim;

    public void SetupAnimationSystem(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAnimation(string animation)
    {
        if (_currentlyPlayingAnim == animation)
        {
            return;
        }
        _animator.Play(animation);
        _currentlyPlayingAnim = animation;
    }

}
