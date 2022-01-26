using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    internal int maxHealth = 100;
    internal int health = 100;
    internal Slider slider;

    [System.NonSerialized] public bool dead = false;

    //Particles
    [SerializeField]
    ParticleSystem bloodParticles;

    private void Update()
    {
        DeadCheck();
    }

    public int GetHealth
    {
        get { return health; }
    }

    // Deal damage.
    public virtual void Damage(int damageAmount)
    {
        health -= damageAmount;
        SetHealthBar(health);

        // If there are no blood particles, Play it.
        if (bloodParticles != null) bloodParticles.Play();
    }

    // Set the healthbar for the Player or Enemy.
    internal void SetHealthBar(int health)
    {
        slider.value = (float)health/maxHealth;
    }

    // Check if the Player or Enemy is dead.
    void DeadCheck()
    {
        if (health <= 0) dead = true;
        else dead = false;
    }

    // Heal the Player or Enemy.
    public void Heal(int healAmount)
    {
        health += healAmount;
        SetHealthBar(health);
        if (health > maxHealth) health = maxHealth;
    }
}