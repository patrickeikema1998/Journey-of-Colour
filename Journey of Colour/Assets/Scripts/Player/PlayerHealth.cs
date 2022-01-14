using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

    private void Start()
    {
        slider = GameObject.Find("Health Panel").GetComponentInChildren<Slider>();
        GameEvents.onRespawnPlayer += HealthReset;
        HealthReset();
    }

    private void Update()
    {
        DeadCheck();
    }

    public override void Damage(int damageAmount)
    {
        base.Damage(damageAmount);
        GetComponentInChildren<PlayerAnimations>().GetHit();
    }

    void HealthReset()
    {
        health = maxHealth;
        SetHealthBar(health);
    }
}
