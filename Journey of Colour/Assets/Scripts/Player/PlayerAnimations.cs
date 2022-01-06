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



    private void Start()
    {
        player = GameObject.Find("Player");
        playerClass = player.GetComponent<SwapClass>();
        playerMovement = player.GetComponent<PlayerMovement>();
        myAnimator = GetComponent<Animator>();

        GameEvents.onPlayerDeath += PlayerDeath;
        GameEvents.onRespawnPlayer += stopDeath;
    }

    private void Update()
    {
        MovementAndIdle();
        if (playerMovement.isGrounded) myAnimator.SetBool("landed", true);
        else myAnimator.SetBool("landed", false);
    }

    void MovementAndIdle()
    {
        float desiredAnimSpeed;

        if ((playerMovement.xAxis > 0.1 || playerMovement.xAxis < -0.1) && playerMovement.isGrounded)
        {
            desiredAnimSpeed = 1;
        }
        else
        {
            desiredAnimSpeed = 0;
        }

        if (desiredAnimSpeed == 0 && myAnimator.GetFloat("speed") < 0.1)
        {
            myAnimator.SetFloat("speed", 0);
        }
        else
        {
            myAnimator.SetFloat("speed", Mathf.Lerp(myAnimator.GetFloat("speed"), desiredAnimSpeed, movementAnimationBlendSpeed * Time.deltaTime));

        }
    }

    public void Jump()
    {


        if (playerMovement.playerClass.IsDevil())
        {
            int randomJump = Random.Range(1, 3);
            myAnimator.SetTrigger("jump" + randomJump);
        }
        else
        {
            myAnimator.SetTrigger("jump");
        }
    }

    public void DoubleJump()
    {
        myAnimator.SetTrigger("doubleJump");
    }

    public void Attack()
    {
        myAnimator.SetTrigger("attack");       
    }

    public void Floating(bool floating)
    {
        if (floating) myAnimator.SetTrigger("startFloat");
        else myAnimator.SetTrigger("stopFloat");
    }

    public void GetHit()
    {
        int randomHit = Random.Range(1, 3);
        myAnimator.SetTrigger("getHit" + randomHit);
    }

    public void Dash()
    {
        myAnimator.SetTrigger("dash");
    }

    public void Death(bool death)
    {
        if (!myAnimator.GetBool("isDead") && death)
        {
            int randomDeath = Random.Range(1, 3);
            myAnimator.SetTrigger("death" + randomDeath);
        }
        myAnimator.SetBool("isDead", death);
    }

    public void RangeAttack()
    {
        myAnimator.SetTrigger("rangeAttack");
    }

    public void PlayerDeath()
    {
        Death(true);

    }
    void stopDeath()
    {
        Death(false);
    }

}
