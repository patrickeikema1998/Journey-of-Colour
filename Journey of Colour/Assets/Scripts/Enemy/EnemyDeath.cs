using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    EnemyHealth health;
    EnemyAnimations anim;
    bool go;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        anim = GetComponent<EnemyAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.dead && !go)
        {
            go = true;
            AudioManager.instance.PlayOrStop(GetComponent<EnemyType>().typeOfEnemy + "Death", true);
            anim.Death();
            Invoke("DestroyGameObj", anim.deathTime);
        }
    }
    void DestroyGameObj()
    {
        Destroy(gameObject);
    }
}
