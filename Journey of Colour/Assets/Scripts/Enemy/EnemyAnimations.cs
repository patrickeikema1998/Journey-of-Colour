using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]float movementAnimationBlendSpeed = 2f;
    Animator myAnimator;
    CharacterController controller;
    [HideInInspector] public float plantAttackAnimTime, spearAttackAnimTime, deathTime = 2;
    [SerializeField] bool isMovingEnemy;


    private void Start()
    {
        spearAttackAnimTime = .5f;
        plantAttackAnimTime = .35f;
        controller = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
       if(isMovingEnemy)MovementAndIdle();
    }

    void MovementAndIdle()
    {
        float desiredAnimSpeed;
        if ((controller.velocity.x > .5f || controller.velocity.x < -.5))
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
