using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1,
                 attackCooldown = 3,
                 attackRange = 2;
    public GameObject player;

    Rigidbody m_Rigidbody;
    MeleeAttack attack;

    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        attack = GetComponent<MeleeAttack>();
        timeLeft = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //zorgt ervoor det de enemy naar de speler wijst
        Vector3 playerDirection = player.transform.position - transform.position;
        transform.forward = new Vector3(playerDirection.x, 0, playerDirection.z);

        //verplaatst de enemy naar voren
        m_Rigidbody.AddForce(transform.forward * speed *Time.deltaTime, ForceMode.VelocityChange);

        //kijkt of de enemy aan kan vallen
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        if (Vector3.SqrMagnitude(playerDirection) < attackRange * attackRange && timeLeft < 0) Attack();
    }

    void Attack()
    {
        timeLeft = attackCooldown;
        attack.Attack();
        Debug.Log("Attacks!");
    }
}
