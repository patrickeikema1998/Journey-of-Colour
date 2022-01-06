using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    [System.NonSerialized] public bool dead = false;

    public Healthbar healthbar;
    //PlayerAnimations playerAnim;
    //EnemyAnimations enemyAnim;
    PlayerMovement playerMovement;
    PlayerAnimations playerAnim;
    EnemyAnimations enemyAnim;


    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "Enemy") enemyAnim = GetComponent<EnemyAnimations>();
        playerMovement = GetComponent<PlayerMovement>();

        HealthReset();
        GameEvents.onRespawnPlayer += HealthReset;
    }

    private void Update()
    {
        if (health <= 0) dead = true;
        else dead = false;
    }

    public int GetHealth
    {
        get { return health; }
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        healthbar.SetHealth(health);

        if (this.gameObject.tag == "Player") playerMovement.PlayerAnim.GetHit();
        if (this.gameObject.tag == "Enemy") enemyAnim.GetHit();
    }

    public void heal(int healAmount)
    {
        health += healAmount;
        healthbar.SetHealth(health);
        if (health > maxHealth) health = maxHealth;
    }

    void HealthReset()
    {
        health = maxHealth;
        healthbar.SetHealth(health);
    }


}