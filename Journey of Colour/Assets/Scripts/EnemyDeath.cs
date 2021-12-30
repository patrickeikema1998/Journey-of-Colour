using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Health health;
    NewEnemyAnimations anim;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        anim = GetComponent<NewEnemyAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health <= 0)
        {
            health.dead = true;
            anim.Death();
            Invoke("DestroyGameObj", anim.deathTime);
        }
    }
    void DestroyGameObj()
    {
        Destroy(gameObject);
    }
}
