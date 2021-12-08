using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    private string currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    void changeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
