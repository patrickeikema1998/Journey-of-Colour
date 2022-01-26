using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    SwapClass playerClass;

    [HideInInspector] public Animator myAnimator;
    float movementAnimationBlendSpeed = 30f;

    int amountOfHitAnims = 3;
    int amountOfDeathAnims = 3;
    int amountOfJumpAnims = 3;
    float maxSpeed = 1f;
    float minimumAxisSpeed = 0.1f;
    float minimumAnimSpeed = 0.1f;

    //animator variable strings
    string speedBoolName = "speed";
    string landedBoolName = "landed";
    string jumpBoolName = "jump";
    string doubleJumpBoolName = "doubleJump";
    string floatingBoolName = "floating";
    string isDeadBoolName = "isDead";
    string rangeAttackTriggerName = "rangeAttack";
    string attackTriggerName = "attack";
    string dashTriggerName = "dash";
    string getHitTriggerName = "getHit";
    string deathTriggerName = "death";










    private void Start()
    {
        player = GameObject.Find(ObjectTags._PlayerTag);
        playerClass = player.GetComponent<SwapClass>();
        playerMovement = player.GetComponent<PlayerMovement>();
        myAnimator = GetComponent<Animator>();

        //this adds the death animation to the death event. the stop death animation function is called so that the player goes back to idle in the animator.
        GameEvents.onPlayerDeath += PlayerDeathAnimation;
        GameEvents.onRespawnPlayer += stopDeathAnimation;
    }

    private void Update()
    {

        //this is the only animation handled in this class, the other animations are handled in the classes with that specific functionality.
        MovementAndIdleAnimation();

        //this just sets the landed bool in the animator on true if the player is grounded.
        if (playerMovement.isGrounded) myAnimator.SetBool(landedBoolName, true);
        else myAnimator.SetBool(landedBoolName, false);
    }

    void MovementAndIdleAnimation()
    {
        float desiredAnimSpeed;
        //for both directions, if the speed is above a certain point, the desired animation speed is 1. else its 0.
        if ((playerMovement.xAxis > minimumAxisSpeed || playerMovement.xAxis < -minimumAxisSpeed) && playerMovement.isGrounded)
        {
            desiredAnimSpeed = maxSpeed;
        }
        else
        {
            desiredAnimSpeed = 0;
        }

        //This makes the movement blending more snappy when the player stops. We agreed on making the animation stop almost instantly.
        if (desiredAnimSpeed == 0 && myAnimator.GetFloat(speedBoolName) < minimumAnimSpeed)
        {
            myAnimator.SetFloat(speedBoolName, 0);
        }
        else
        {
            myAnimator.SetFloat(speedBoolName, Mathf.Lerp(myAnimator.GetFloat(speedBoolName), desiredAnimSpeed, movementAnimationBlendSpeed * Time.deltaTime));

        }
    }

    public void JumpAnimation()
    {


        if (playerClass.IsDevil())
        {
            //if the player is a devil, theres a random jump animation.
            int randomJump = Random.Range(1, amountOfJumpAnims);
            myAnimator.SetTrigger(jumpBoolName + randomJump);
        }
        else
        {
            //the angel class has a standard jump animation, because it also has a standard double jump animation.
            myAnimator.SetTrigger(jumpBoolName);
        }
    }

    public void DoubleJumpAnimation()
    {
        myAnimator.SetTrigger(doubleJumpBoolName);
    }

    public void AttackAnimation()
    {
        myAnimator.SetTrigger(attackTriggerName);
    }

    public void FloatingAnimation(bool floating)
    {
        //sets the bool on true or false
        if ((floating && !myAnimator.GetBool(floatingBoolName)) || (!floating && myAnimator.GetBool(floatingBoolName)))
        {
            myAnimator.SetBool(floatingBoolName, floating);
        }
    }

    public void GetHitAnimation()
    {
        int randomHit = Random.Range(1, amountOfHitAnims);
        myAnimator.SetTrigger(getHitTriggerName + randomHit);
    }

    public void DashAnimation()
    {
        myAnimator.SetTrigger(dashTriggerName);
    }

    public void DeathAnimation(bool death)
    {
        //if the player dies, theres a random death anim.
        if (!myAnimator.GetBool(isDeadBoolName) && death)
        {
            int randomDeath = Random.Range(1, amountOfDeathAnims);
            myAnimator.SetTrigger(deathTriggerName + randomDeath);
        }
        //this bool is to stop other animations to happen.
        myAnimator.SetBool(isDeadBoolName, death);
    }

    public void RangeAttackAnimation()
    {
        myAnimator.SetTrigger(rangeAttackTriggerName);
    }

    public void PlayerDeathAnimation()
    {
        DeathAnimation(true);

    }
    void stopDeathAnimation()
    {
        DeathAnimation(false);
    }

}
