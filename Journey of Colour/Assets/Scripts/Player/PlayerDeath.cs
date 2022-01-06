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
        deathTimer.start = false;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer.Update();

        if (health.dead && !deathTimer.start)
        {
            //starts death event of player
            GameEvents.PlayerDeath();
            deathTimer.start = true;      
        }
        if (deathTimer.finish)
        {
            //start respawn event
            deathTimer.Reset();
            deathTimer.start = false;
            GameEvents.RespawnPlayer();
        }
    }
}
