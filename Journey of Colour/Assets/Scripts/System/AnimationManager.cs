using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private string currentState;

    public void PlayAnimation(Animator animator,string newState)
    {
        if (currentState != newState) //to prevent animation from spamming
        {
            animator.Play(newState);
            currentState = newState;
        }
    }
}
