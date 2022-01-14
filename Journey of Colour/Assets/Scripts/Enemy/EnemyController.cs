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
    float distance, minimumDistance;
    [SerializeField][Tooltip("This will be used for sounds")] string typeOfEnemy;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        anim = GetComponent<EnemyAnimations>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<MeleeAttack>();
        health = GetComponent<EnemyHealth>();
        timeLeft = attackCooldown;
        minimumDistance = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //zorgt ervoor dat de enemy naar de speler wijst
        Vector3 playerDirection = player.transform.position - transform.position;
        //kijkt of de enemy aan kan vallen
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        if (Vector3.SqrMagnitude(playerDirection) < attackDetectionRange * attackDetectionRange && timeLeft < 0 && !health.dead) Attack();

    }

    void Movement()
    {
   

        //verplaatst de enemy naar voren
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < enemySight && !health.dead && distance > minimumDistance)
        {
            controller.SimpleMove(transform.forward * speed);
        }
        else
        {
            //make it stop so that animations also stop.
            controller.Move(Vector3.zero);
        }
    }

    void Attack()
    {
        AudioManager.instance.PlayOrStop( typeOfEnemy + "Attack" , true);
        anim.Attack();
        timeLeft = attackCooldown;
        Invoke("DoAttack", anim.plantAttackAnimTime);
    }
    void DoAttack()
    {
        attack.Attack();
    }


}
