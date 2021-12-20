using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    AnimationManager animationManager;
    Animator anim;
    CharacterController charController;

    const string walk = "walk";
    const string idle = "idle";
    const string attack = "attack";
    const string death = "death";
    const string getHit = "gethit";

    bool isAttacking;
    bool isGettingHit;



    float attackAnimLengthMelee = .8f;
    float gettingHItAnimLength = 1f;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        DoWalkAnimation();
        DoIdleAnimation();
    }

    private void DoWalkAnimation()
    {
        if (charController.velocity.x != 0 && !isAttacking && !isGettingHit) animationManager.PlayAnimation(anim, walk);
    }

    private void DoIdleAnimation()
    {
        if (charController.velocity.x == 0 && !isAttacking && !isGettingHit) animationManager.PlayAnimation(anim, idle);
    }

    public void DoAttackAnimation()
    {
        isAttacking = true;
        animationManager.PlayAnimation(anim, attack);
        Invoke("AttackComplete", attackAnimLengthMelee);
    }

    public void DoDeathAnimation()
    {
        animationManager.PlayAnimation(anim, death);
    }

    public void DoGetHitAnimation()
    {
        isGettingHit = true;
        animationManager.PlayAnimation(anim, getHit);
        Invoke("GettingHitComplete", gettingHItAnimLength);

    }

    private void AttackComplete()
    {
        isAttacking = false;
    }

    private void GettingHitComplete()
    {
        isGettingHit = false;
    }
}

