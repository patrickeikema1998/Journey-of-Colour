using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 1, attackCooldown = 3, attackDetectionRange = 2, enemySight;
    GameObject player;

    public CharacterController controller;
    EnemyAnimations anim;
    MeleeAttack attack;
    EnemyHealth health;
    float timeLeft;
    float distance;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<EnemyAnimations>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<MeleeAttack>();
        health = GetComponent<EnemyHealth>();
        timeLeft = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //zorgt ervoor det de enemy naar de speler wijst
        Vector3 playerDirection = player.transform.position - transform.position;
        //transform.forward = new Vector3(playerDirection.x, 0, playerDirection.z);

        //verplaatst de enemy naar voren
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < enemySight && !health.dead)
        {
            controller.SimpleMove(transform.forward * speed);
        }

        //kijkt of de enemy aan kan vallen
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        if (Vector3.SqrMagnitude(playerDirection) < attackDetectionRange * attackDetectionRange && timeLeft < 0 && !health.dead) Attack();

    }

    void Attack()
    {
        anim.Attack();
        timeLeft = attackCooldown;
        Invoke("DoAttack", anim.plantAttackAnimTime);
    }
    void DoAttack()
    {
        attack.Attack();
    }


}
