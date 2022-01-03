using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private string currentState;
    private Animator currentAnimator;
    public bool forceOverride;
    public void PlayAnimation(Animator animator,string newState)
    {
        if (currentAnimator != animator || currentState != newState) //to prevent animation from spamming
        {
            currentAnimator = animator;
            currentState = newState;
            animator.Play(newState);
            forceOverride = false;
        }
    }
}
