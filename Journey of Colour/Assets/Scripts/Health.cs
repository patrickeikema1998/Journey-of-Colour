using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;

    public int health;

    [System.NonSerialized] public bool dead = false;

    public GameObject player;
    public Healthbar healthbar;
    PlayerAnimations playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        playerAnim = GetComponent<PlayerAnimations>();
    }

    public int GetHealth
    {
        get { return health; }
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        healthbar.SetHealth(health);
        playerAnim.GettingHit();
        if (health <= 0)
        {
            dead = true;
        }
    }

    public void heal(int healAmount)
    {
        health += healAmount;
        healthbar.SetHealth(health);
        if (health > maxHealth) health = maxHealth;
        dead = false;
    }

    private void Update()
    {
        if (dead && gameObject != player)
        {
            Destroy(gameObject);
        }

        if(health <= 0 && gameObject == player)
        {
            health = maxHealth;
            healthbar.SetHealth(health);
        }
    }
}
