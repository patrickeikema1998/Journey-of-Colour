using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerAnimations : MonoBehaviour
{
    PlayerMovement playerMovement;
    [HideInInspector] SwapClass playerClass;
    AnimationManager animationManager;
    [SerializeField] Animator animatorAngel, animatorDevil;
    Animator currentAnimator;

    //these are all the animation names
    const string jumpVariant1 = "character_jump";
    const string jumpVariant2 = "character_jump_2";
    const string idle = "character_idle";
    const string hit = "character_attack_1";
    const string gettingHitVariant1 = "character_get_hit";
    const string gettingHitVariant2 = "character_get_hit_2";
    const string floating = "float";
    const string fireball = "fireball";
    const string run = "character_run";
    //changable string to handle animationvariants.
    string lastJump, lastGettingHit;

    //time animation takes, so that idle animation wont play while attacking
    const float attackAnimationTime = 0.5f;
    const float fireBallAnimationTime = 0.2f;
    const float gettingHitAnimationTime = 0.7f;

    bool isAttacking;
    bool gettingHit;
    bool isRunning;
    bool isIdle;
    bool isJumping;
    [HideInInspector] public bool isFloating;


    bool shouldJump;


    private void Start()
    {
        shouldJump = false;

        lastGettingHit = gettingHitVariant2;
        lastJump = jumpVariant2;
        animationManager = GetComponent<AnimationManager>();
        playerClass = GetComponent<SwapClass>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        ChangeAnimatorOnClassSwap();

        DoIdleAnimation();
        DoJumpAnimation();
        DoRunAnimation();
        DoHitAnimation();
        DoFloatAnimation();
        DoFireballAnimation();
    }

    void ChangeAnimatorOnClassSwap()
    {
        if (playerClass.IsAngel()) currentAnimator = animatorAngel;
        else currentAnimator = animatorDevil;
    }

    void DoJumpAnimation()
    {

        if (Input.GetKeyDown(KeyCode.Space)) shouldJump = true;
        //this statement checks if player should do an animation
        if (playerMovement.rb.velocity.y > 0 && shouldJump)
        {
            isJumping = true;
            //swaps variants
            if (lastJump == jumpVariant2)
            {
                shouldJump = false;
                animationManager.PlayAnimation(currentAnimator, jumpVariant1);
                lastJump = jumpVariant1;
            }
            else
            {
                shouldJump = false;
                animationManager.PlayAnimation(currentAnimator, jumpVariant2);
                lastJump = jumpVariant2;
            }

        }
    }

    public void DoRunAnimation()
    {
        //checks if player is running.
        if (playerMovement.xAxis != 0 && playerMovement.canJump && playerMovement.isGrounded) isRunning = true;
        else isRunning = false;

        if (!gettingHit && !isAttacking && isRunning) animationManager.PlayAnimation(currentAnimator, run);
    }

    void DoIdleAnimation()
    {
        //checks if player is standing still. isJumping is added, because the y velocity is sometimes 0 in mid-air if the player jumped.
        if (!isJumping && !isAttacking && !isRunning && !gettingHit)
        {
            animationManager.PlayAnimation(currentAnimator, idle);
        }
    }

    private void DoHitAnimation()
    {
       
        if (Input.GetKey(KeyCode.Mouse0) && playerClass.IsDevil() && !isAttacking)
        {
            isAttacking = true;
            animationManager.PlayAnimation(currentAnimator, hit);
            Invoke("AttackComplete", attackAnimationTime);
        }

    }
    private void DoFireballAnimation()
    {

        if (Input.GetKey(KeyCode.Mouse1) && playerClass.IsDevil() && !isAttacking)
        {
            isAttacking = true;
            animationManager.PlayAnimation(currentAnimator, fireball);
            Invoke("AttackComplete", fireBallAnimationTime);
        }
    }
    public void DoGetHitAnimation()
    {
        gettingHit = true;
        if (playerClass.IsAngel())
        {
            //angel has variants
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
        }
        else
        {
            //devil has no variants
            animationManager.PlayAnimation(currentAnimator, gettingHitVariant1);
        }
        //other animations can play after delay
        Invoke("HitComplete", gettingHitAnimationTime);
    }

    private void DoFloatAnimation()
    {
        if (isFloating && !gettingHit)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") isJumping = false;
    }
}
