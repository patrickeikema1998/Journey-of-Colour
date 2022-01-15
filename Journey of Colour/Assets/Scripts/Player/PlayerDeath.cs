using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    PlayerHealth health;
    public int deathAnimTime;
    CustomTimer deathTimer;
    SwapClass playerClass;
    Vector2 angelPitch, devilPitch;
    bool deathStarted;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = GetComponent<SwapClass>();
        deathAnimTime = 4;
        deathTimer = new CustomTimer(deathAnimTime);
        deathTimer.start = false;
        health = GetComponent<PlayerHealth>();
        angelPitch = new Vector2(1.2f, 1.3f);
        devilPitch = new Vector2(1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer.Update();

        if (health.dead && !deathTimer.start && !deathStarted)
        {
            //starts death event of player
            GameEvents.PlayerDeath();
            deathTimer.start = true;
            deathStarted = true;
            if (playerClass.IsAngel()) AudioManager.instance.PlayOrStop("death", true, angelPitch);
            else AudioManager.instance.PlayOrStop("death", true, devilPitch);
        }
        if (deathTimer.finish)
        {
            //start respawn event
            deathTimer.Reset();
            deathTimer.start = false;
            GameEvents.RespawnPlayer();
            deathStarted = false;
        }
    }
}
