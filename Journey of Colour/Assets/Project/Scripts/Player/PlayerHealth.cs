using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    SwapClass playerClass;
    Vector2 angelPitch, devilPitch;
    [SerializeField] AudioSource damageSound;
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
        if ((health - damageAmount) > 0) damageSound.Play();
        base.Damage(damageAmount);
        GetComponentInChildren<PlayerAnimations>().GetHitAnimation();

        //sounds
        if (playerClass.IsAngel()) damageSound.pitch = Random.Range(angelPitch.x, angelPitch.y);
        else damageSound.pitch = Random.Range(devilPitch.x, devilPitch.y);
    }

    void HealthReset()
    {
        health = maxHealth;
        SetHealthBar(health);
    }
}
