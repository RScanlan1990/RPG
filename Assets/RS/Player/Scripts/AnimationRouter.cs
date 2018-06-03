using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRouter : MonoBehaviour {

    private static Animator _animator;
    private Rigidbody _rigidBody;
    public AnimationEvent CurrentEvent;

    private Vector3 previous;
    private float velocity;

    private void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        _animator.SetFloat("Speed", velocity);
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
