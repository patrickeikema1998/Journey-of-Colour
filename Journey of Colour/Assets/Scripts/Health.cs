using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    [System.NonSerialized] public bool dead = false;

    GameObject player;
    public Healthbar healthbar;
    //PlayerAnimations playerAnim;
    //EnemyAnimations enemyAnim;
    PlayerMovement playerMovement;
    NewEnemyAnimations enemyAnim;
    NewPlayerAnimations playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = GetComponent<PlayerMovement>();
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        if (gameObject.tag == "Player") playerAnim = GetComponent<NewPlayerAnimations>();
        else if (gameObject.tag == "Enemy") enemyAnim = GetComponent<NewEnemyAnimations>();

        GameEvents.onRespawnPlayer += PlayerHealthReset;
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
    }

    public void heal(int healAmount)
    {
        health += healAmount;
        healthbar.SetHealth(health);
        if (health > maxHealth) health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0 && gameObject != player)
        {
            EnemyDeath();
        }

        if(health <= 0 && gameObject == player)
        {
            GameEvents.PlayerDeath();
            //Invoke("InvokePlayerRespawn", playerAnim.deathAnimTime);
            GameEvents.RespawnPlayer();
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
        dead = true;
        enemyAnim.Death();
        Invoke("DestroyGameObj", enemyAnim.deathTime);
    }

    void DestroyGameObj()
    {
        Destroy(gameObject);
    }
}
