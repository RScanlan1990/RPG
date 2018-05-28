using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRouter : MonoBehaviour {

    private static Animator _animator;
    public AnimationEvent CurrentEvent;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void  SetAnimationTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    private void SetAnimationBool(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    public enum AnimationEvent
    {
        Idle,
        Cast,
        MidCast,
        CastEnd,
        FishingEnd
    }

    public bool AnimationEventRouter(AnimationEvent animationEvent)
    {
        switch (animationEvent)
        {
            case AnimationEvent.Cast:
                CurrentEvent = animationEvent;
                SetAnimationTrigger("Cast");
                SetAnimationBool("Fishing", true);
                return true;
            case AnimationEvent.MidCast:
                CurrentEvent = animationEvent;
                return true;
            case AnimationEvent.FishingEnd:
                CurrentEvent = animationEvent;
                SetAnimationBool("Fishing", false);
                return true;
            default:
                return false;
        }
    }
}
