using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 1, attackCooldown = 3, attackDetectionRange = 2, enemySight;
    [SerializeField] GameObject player;

    public CharacterController controller;
    MeleeAttack attack;
    Health health;
    MeleeEnemyAnimations animations;

    float timeLeft;

    float distance;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        attack = GetComponent<MeleeAttack>();
        health = GetComponent<Health>();
        timeLeft = attackCooldown;
        animations = GetComponent<MeleeEnemyAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.dead) Die();

        //zorgt ervoor det de enemy naar de speler wijst
        Vector3 playerDirection = player.transform.position - transform.position;
        //transform.forward = new Vector3(playerDirection.x, 0, playerDirection.z);

        //verplaatst de enemy naar voren
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < enemySight)
        {
            controller.SimpleMove(transform.forward * speed);
        }

        //kijkt of de enemy aan kan vallen
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        if (Vector3.SqrMagnitude(playerDirection) < attackDetectionRange * attackDetectionRange && timeLeft < 0) Attack();
    }

    void Attack()
    {
        timeLeft = attackCooldown;
        attack.Attack();
        animations.DoAttackAnimation();

    }

    void Die()
    {
        Destroy(gameObject);
    }
}
