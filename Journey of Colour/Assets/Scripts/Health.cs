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
    //PlayerAnimations playerAnim;
    //EnemyAnimations enemyAnim;
    PlayerMovement playerMovement;
    NewEnemyAnimations enemyAnim;
    NewPlayerAnimations playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        //if (this.gameObject.tag == "Player")  playerAnim = GetComponent<PlayerAnimations>();
        // if (this.gameObject.tag == "Enemy") enemyAnim = GetComponent<EnemyAnimations>();
        if (gameObject.tag == "Player")
        {
            GameEvents.onRespawnPlayer += PlayerHealthReset;
            playerAnim = GetComponent<NewPlayerAnimations>();
        }
        else if (gameObject.tag == "Enemy") enemyAnim = GetComponent<NewEnemyAnimations>();
    }

    public int GetHealth
    {
        get { return health; }
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        healthbar.SetHealth(health);

        if(this.gameObject.tag == "Player") playerMovement.PlayerAnim.GetHit();
        if (this.gameObject.tag == "Enemy") enemyAnim.GetHit();
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
            enemyAnim.Death();
            Invoke("EnemyDeath", enemyAnim.deathTime);
        }

        if(health <= 0 && gameObject == player)
        {
            GameEvents.PlayerDeath();
            Invoke("InvokePlayerRespawn", playerAnim.deathAnimTime);
            //GameEvents.RespawnPlayer();
        }
    }

    void PlayerHealthReset()
    {
        health = maxHealth;
        healthbar.SetHealth(health);
    }

    void InvokePlayerRespawn()
    {
        GameEvents.RespawnPlayer();
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }

}
