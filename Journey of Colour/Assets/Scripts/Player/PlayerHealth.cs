using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    SwapClass playerClass;
    Vector2 angelPitch, devilPitch;
    private void Start()
    {
        slider = GameObject.Find("Health Panel").GetComponentInChildren<Slider>();
        GameEvents.onRespawnPlayer += HealthReset;
        HealthReset();
        playerClass = GetComponent<SwapClass>();
        angelPitch = new Vector2(1.2f, 1.3f);
        devilPitch = new Vector2(1f, 1f);
    }

    private void Update()
    {
        DeadCheck();
    }

    public override void Damage(int damageAmount)
    {
        base.Damage(damageAmount);
        GetComponentInChildren<PlayerAnimations>().GetHit();

        if (playerClass.IsAngel()) AudioManager.instance.PlayOrStop("getHit", true, angelPitch);
        else AudioManager.instance.PlayOrStop("getHit", true, devilPitch);

    }

    void HealthReset()
    {
        health = maxHealth;
        SetHealthBar(health);
    }
}
