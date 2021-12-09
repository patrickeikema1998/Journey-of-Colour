using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    SwapClass playerClass;
    AnimationManager animationManager;
    [SerializeField] Animator animatorAngel, animatorDevil;
    Animator currentAnimator;

    string jumpVariant1 = "character_jump";
    string jumpVariant2 = "character_jump_2";
    string idle = "character_idle";
    string run = "character_run";

    string lastJump;

    private void Start()
    {
        lastJump = jumpVariant2;
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        playerClass = GameObject.Find("Player").GetComponent<SwapClass>();
    }

    private void Update()
    {
        if (playerClass.currentClass == SwapClass.playerClasses.Angel) currentAnimator = animatorAngel;
        else currentAnimator = animatorDevil;
    }


    public void JumpAnimation()
    {
            if (lastJump == jumpVariant2)
            {
                animationManager.changeAnimationState(currentAnimator, jumpVariant1);
                lastJump = jumpVariant1;
            }
            else
            {
                animationManager.changeAnimationState(currentAnimator, jumpVariant2);
                lastJump = jumpVariant2;
            }
    }

    public void IdleAnimation()
    {
        animationManager.changeAnimationState(currentAnimator, idle);
    }

    public void RunAnimation()
    {
        animationManager.changeAnimationState(currentAnimator, run);
    }
}
