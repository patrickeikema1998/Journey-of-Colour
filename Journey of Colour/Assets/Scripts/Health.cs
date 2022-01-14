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

    public int GetHealth
    {
        get { return health; }
    }

    public virtual void Damage(int damageAmount)
    {
        health -= damageAmount;
        SetHealthBar(health);
    }

    internal void SetHealthBar(int health)
    {
        slider.value = (float)health/maxHealth;
    }

    internal void DeadCheck()
    {
        if (health <= 0) dead = true;
        else dead = false;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        SetHealthBar(health);
        if (health > maxHealth) health = maxHealth;
    }
}