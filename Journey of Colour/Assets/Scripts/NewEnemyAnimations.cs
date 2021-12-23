using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyAnimations : MonoBehaviour
{
    [SerializeField]float movementAnimationBlendSpeed = 2f;
    Animator myAnimator;
    CharacterController controller;

    public float attackAnimTime = 0.001f, deathTime = 2;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAndIdle();
    }

    void MovementAndIdle()
    {
        float desiredAnimSpeed;
        if ((controller.velocity.x > 0.1 || controller.velocity.x < -0.1))
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
    public void Attack()
    {
        myAnimator.SetTrigger("attack");
    }

    public void GetHit()
    {
        myAnimator.SetTrigger("getHit");
    }

    public void Death()
    {
        if(!myAnimator.GetBool("isDead"))
        myAnimator.SetBool("isDead", true);
    }
}
