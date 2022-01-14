using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    EnemyHealth health;
    EnemyAnimations anim;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        anim = GetComponent<EnemyAnimations>();
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
