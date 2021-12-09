using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private string currentState;


    public void changeAnimationState(Animator animator,string newState)
    {
        if (currentState != newState)
        {
            animator.Play(newState);
            currentState = newState;
        }
    }
}
