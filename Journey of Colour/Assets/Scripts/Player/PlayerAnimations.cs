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

    const string jumpVariant1 = "character_jump";
    const string jumpVariant2 = "character_jump_2";
    const string idle = "character_idle";
    const string hit = "character_attack_1";
    const string gettingHitVariant1 = "character_get_hit";
    const string gettingHitVariant2 = "character_get_hit_2";
    const string floating = "float";
    string run;


    string lastJump, lastGettingHit;
    bool isAttacking;
    bool gettingHit;
    bool isRunning;
    const float attackDelay = 0.6f;

    private void Start()
    {
        lastGettingHit = gettingHitVariant2;
        lastJump = jumpVariant2;
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        playerClass = GetComponent<SwapClass>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        //changes animator if class is swapped.
        if (playerClass.currentClass == SwapClass.playerClasses.Angel)
        {
            currentAnimator = animatorAngel;
            //ugly fix for animator
            run = "character_run_angel";

        }
        else
        {
            currentAnimator = animatorDevil;
            run = "character_run_devil";
        }


        JumpAnimation();
        RunAnimation();
        IdleAnimation();
        Hit();
        Floating();
    }


    void JumpAnimation()
    {
        if ( Input.GetKey(KeyCode.Space) && playerMovement.isGrounded)
        {
            if (lastJump == jumpVariant2)
            {
                animationManager.PlayAnimation(currentAnimator, jumpVariant1);
                lastJump = jumpVariant1;
            }
            else
            {
                animationManager.PlayAnimation(currentAnimator, jumpVariant2);
                lastJump = jumpVariant2;
            }
        }
    }

    public void RunAnimation()
    {
        if (playerMovement.xAxis != 0 && playerMovement.isGrounded && playerMovement.rb.velocity.y == 0) isRunning = true;
        else isRunning = false;
        if (!gettingHit && !isAttacking && isRunning) animationManager.PlayAnimation(currentAnimator, run);
    }

     void IdleAnimation()
    {
        if (playerMovement.isGrounded && playerMovement.rb.velocity.y == 0 && !isAttacking && !isRunning && !gettingHit) animationManager.PlayAnimation(currentAnimator, idle);
    }

    private void Hit()
    {

        if (Input.GetKey(KeyCode.Mouse0) && playerClass.currentClass == SwapClass.playerClasses.Devil && !isAttacking)
        {
            isAttacking = true;
            animationManager.PlayAnimation(currentAnimator, hit);
            Invoke("AttackComplete", attackDelay);
        }

    }

    public void GettingHit()
    {
        gettingHit = true;
        if (playerClass.currentClass == SwapClass.playerClasses.Angel)
        {
            if (lastGettingHit == gettingHitVariant2) 
            {
                animationManager.PlayAnimation(currentAnimator, gettingHitVariant1);
                lastGettingHit = gettingHitVariant1;
            } 
            else
            {
                animationManager.PlayAnimation(currentAnimator, gettingHitVariant2);
                lastGettingHit = gettingHitVariant1;
            }
        } else
        {
            animationManager.PlayAnimation(currentAnimator, gettingHitVariant1);
        }

        Invoke("HitComplete", 1f);
    }

    private void Floating()
    {
        if(playerMovement.rb.velocity.y == 0 && !playerMovement.isGrounded && playerClass.currentClass == SwapClass.playerClasses.Angel && !gettingHit)
        {
            animationManager.PlayAnimation(currentAnimator, floating);
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void HitComplete()
    {

        gettingHit = false;
    }
}
