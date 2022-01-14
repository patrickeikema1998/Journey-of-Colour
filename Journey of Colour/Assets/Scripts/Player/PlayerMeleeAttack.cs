using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MeleeAttack
{

    PlayerAnimations anim;
    PlayerHealth health;
    CustomTimer meleeAttackCooldownTimer;
    [SerializeField] float meleeAttackCDInSeconds;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<PlayerAnimations>();
        health = transform.parent.gameObject.GetComponent<PlayerHealth>();
        attackBox = new Vector3(attackRange / 2, attackRange / 4, attackRange / 2);
        meleeAttackCooldownTimer = new CustomTimer(meleeAttackCDInSeconds);
        meleeAttackCooldownTimer.start = true;
    }

    // Update is called once per frame
    void Update()
    {
        meleeAttackCooldownTimer.Update();
        Attack();
    }


    public override void Attack()
    {
        if (Input.GetKeyDown(GameManager.GM.meleeAbility) && meleeAttackCooldownTimer.finish && !health.dead)
        {
            base.Attack();
            AudioManager.instance.PlayOrStop("attack" + Random.Range(1, 4), true);
            meleeAttackCooldownTimer.Reset();
            anim.Attack();
        }
    }
}

