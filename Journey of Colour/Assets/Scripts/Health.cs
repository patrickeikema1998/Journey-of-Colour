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
    EnemyAnimations enemyAnim;
    PlayerAnimations playerAnim;
    public int deathAnimTime;
    CustomTimer deathTimer;


    // Start is called before the first frame update
    void Start()
    {
        deathAnimTime = 4;
        deathTimer = new CustomTimer(deathAnimTime);
        player = GameObject.Find("Player");
        playerMovement = GetComponent<PlayerMovement>();
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        if (this.gameObject.tag == "Enemy") enemyAnim = GetComponent<EnemyAnimations>();

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
        deathTimer.Update();
        if (health <= 0 && gameObject != player)
        {
            EnemyDeath();
        }

        if(gameObject == player) 
        {
            if (health <= 0)
            {
                dead = true;
                GameEvents.PlayerDeath();
                deathTimer.start = true;

                if (deathTimer.finish)
                {
                    deathTimer.Reset();
                    deathTimer.start = false;
                    InvokePlayerRespawn();
                }
                //Invoke("InvokePlayerRespawn", deathAnimTime);

            }
            else dead = false;
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
