using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    [SerializeField] Slider slider;

    [System.NonSerialized] public bool dead = false;

    //PlayerAnimations playerAnim;
    //EnemyAnimations enemyAnim;
    PlayerMovement playerMovement;
    PlayerAnimations playerAnim;
    EnemyAnimations enemyAnim;



    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "Enemy") enemyAnim = GetComponent<EnemyAnimations>();
        if(this.gameObject.tag == "Player") playerMovement = GetComponent<PlayerMovement>();
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
        SetHealthBar(health);

        if (this.gameObject.tag == "Player" && playerMovement != null) GetComponentInChildren<PlayerAnimations>().GetHit();
        if (this.gameObject.tag == "Enemy" && enemyAnim != null) enemyAnim.GetHit();
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        SetHealthBar(health);
        if (health > maxHealth) health = maxHealth;
    }

    void HealthReset()
    {
        health = maxHealth;
        SetHealthBar(health);
    }

    public void SetHealthBar(int health)
    {
        slider.value = (float)health/maxHealth;
    }

}