using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement playerMovement;
    SwapClass playerClass;
    AnimationManager animationManager;
    [SerializeField] Animator animatorAngel, animatorDevil;
    Animator currentAnimator;

    string jumpVariant1 = "character_jump";
    string jumpVariant2 = "character_jump_2";
    string idle = "character_idle";
    string run = "character_run";
    string hit = "character_attack_1";

    string lastJump;
    bool isAttacking;
    const float attackTime = 0.6f;

    private void Start()
    {
        lastJump = jumpVariant2;
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        playerClass = GetComponent<SwapClass>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        //changes animator if class is swapped.
        if (playerClass.currentClass == SwapClass.playerClasses.Angel) currentAnimator = animatorAngel;
        else currentAnimator = animatorDevil;

        JumpAnimation();
        IdleAndRunAnimation();
        Hit();

    }


    void JumpAnimation()
    {
        if ( Input.GetKey(KeyCode.Space) && playerMovement.isGrounded)
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
    }

    void IdleAndRunAnimation()
    {
        //run
        if (playerMovement.xAxis != 0 && playerMovement.isGrounded && playerMovement.rb.velocity.y == 0 && !isAttacking) animationManager.changeAnimationState(currentAnimator, run);
        //idle
        else if (playerMovement.isGrounded && playerMovement.rb.velocity.y == 0 && !isAttacking) animationManager.changeAnimationState(currentAnimator, idle);
    }

    private void Hit()
    {

        if (Input.GetKey(KeyCode.Mouse0) && playerClass.currentClass == SwapClass.playerClasses.Devil && !isAttacking)
        {
            isAttacking = true;
            animationManager.changeAnimationState(currentAnimator, hit);
            Invoke("AttackComplete", attackTime);
        }

    }

    void AttackComplete()
    {
        isAttacking = false;
    }
}
