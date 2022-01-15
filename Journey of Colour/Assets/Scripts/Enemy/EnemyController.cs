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
    PlayerHealth playerHealth;
    float timeLeft;
    float distance, minimumDistance;
    Vector2 randomSoundPitch = new Vector2(1, 1.31f);



    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        anim = GetComponent<EnemyAnimations>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<MeleeAttack>();
        health = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
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
        if (Vector3.SqrMagnitude(playerDirection) < attackDetectionRange * attackDetectionRange && timeLeft < 0 && !health.dead && !playerHealth.dead) Attack();

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
        AudioManager.instance.PlayOrStop(GetComponent<EnemyType>().typeOfEnemy + "Attack" , true, randomSoundPitch);
        anim.Attack();
        timeLeft = attackCooldown;
        Invoke("DoAttack", anim.plantAttackAnimTime);
    }
    void DoAttack()
    {
        attack.Attack();
    }


}
