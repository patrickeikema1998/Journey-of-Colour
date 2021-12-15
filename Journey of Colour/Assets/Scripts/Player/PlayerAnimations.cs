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
    const string fireball = "fireball";

    string run;


    string lastJump, lastGettingHit;
    bool isAttacking;
    bool gettingHit;
    bool isRunning;
    bool isIdle;
    [HideInInspector] public bool isFloating;
    const float attackAnimationTime = 0.5f;
    const float gettingHitAnimationTime = 0.7f;


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


        DoJumpAnimation();
        DoRunAnimation();
        DoIdleAnimation();
        DoHitAnimation();
        DoFloatAnimation();
        DoFireballAnimation();
    }


    void DoJumpAnimation()
    {
        if ( Input.GetKey(KeyCode.Space) && playerMovement.canJump)
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

    public void DoRunAnimation()
    {
        if (playerMovement.xAxis != 0 && playerMovement.canJump && playerMovement.rb.velocity.y == 0) isRunning = true;
        else isRunning = false;
        if (!gettingHit && !isAttacking && isRunning) animationManager.PlayAnimation(currentAnimator, run);
    }

     void DoIdleAnimation()
    {
        if (playerMovement.canJump && playerMovement.rb.velocity.y == 0 && !isAttacking && !isRunning && !gettingHit)
        {
            if (!isIdle)
            {
                animationManager.PlayAnimation(currentAnimator, idle);
                isIdle = true;
            }
        } else
        {
            isIdle = false;
        }
    }

    private void DoHitAnimation()
    {

        if (Input.GetKey(KeyCode.Mouse0) && playerClass.currentClass == SwapClass.playerClasses.Devil && !isAttacking)
        {
            isAttacking = true;
            animationManager.PlayAnimation(currentAnimator, hit);
            Invoke("AttackComplete", attackAnimationTime);
        }

    }
    private void DoFireballAnimation()
    {

        if (Input.GetKey(KeyCode.Mouse1) && playerClass.currentClass == SwapClass.playerClasses.Devil && !isAttacking)
        {
            isAttacking = true;
            animationManager.PlayAnimation(currentAnimator, fireball);
            Invoke("AttackComplete", attackAnimationTime);
        }
    }
    public void DoGetHitAnimation()
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

        Invoke("HitComplete", gettingHitAnimationTime);
    }

    private void DoFloatAnimation()
    {
        if(isFloating && !gettingHit)
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
