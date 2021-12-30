using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Health health;
    public int deathAnimTime;
    CustomTimer deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        deathAnimTime = 4;
        deathTimer = new CustomTimer(deathAnimTime);
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer.Update();

        if (health.health <= 0)
        {
            //starts death of player
            GameEvents.PlayerDeath();
            deathTimer.start = true;

            //if timer's finished, start respawn
            if (deathTimer.finish)
            {
                deathTimer.Reset();
                deathTimer.start = false;
                InvokePlayerRespawn();
            }
        }

    }


    void InvokePlayerRespawn()
    {
        GameEvents.RespawnPlayer();
    }
}
