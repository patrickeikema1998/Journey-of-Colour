using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]float movementAnimationBlendSpeed = 2f;
    [HideInInspector] public float deathTime = 2;
    [SerializeField] bool isMovingEnemy;
    public float attackAnimTime;
    Animator myAnimator;
    CharacterController controller;

    float minimumMoveVelocity = .5f;
    float minimumAnimSpeed = 0.1f;

    const string animSpeedFloat = "speed";
    const string animIsDeadBool = "isDead";
    const string animAttackTrigger = "attack";
    const string animgetHitTrigger = "isDead";



    private void Start()
    {
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
        if ((controller.velocity.x > minimumMoveVelocity || controller.velocity.x < -minimumMoveVelocity))
        {
            desiredAnimSpeed = 1;
        }
        else
        {
            desiredAnimSpeed = 0;
        }
        if (desiredAnimSpeed == 0 && myAnimator.GetFloat(animSpeedFloat) < minimumAnimSpeed)
        {
            myAnimator.SetFloat(animSpeedFloat, 0);
        }
        else
        {
            myAnimator.SetFloat(animSpeedFloat, Mathf.Lerp(myAnimator.GetFloat(animSpeedFloat), desiredAnimSpeed, movementAnimationBlendSpeed * Time.deltaTime));

        }
    }
    public void Attack()
    {
        myAnimator.SetTrigger(animAttackTrigger);
    }

    public void GetHit()
    {
        myAnimator.SetTrigger(animgetHitTrigger);
    }

    public void Death()
    {
        if(!myAnimator.GetBool(animIsDeadBool))
        myAnimator.SetBool(animIsDeadBool, true);
    }
}
