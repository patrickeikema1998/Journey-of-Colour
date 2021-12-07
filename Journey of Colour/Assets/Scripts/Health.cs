using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int maxHealth = 10;

    public Rigidbody rb;

    public int health;

    public GameObject panel;

    [System.NonSerialized] public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public int GetHealth
    {
        get { return health; }
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            dead = true;
        }
    }

    public void heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
        dead = false;
    }

    private void Update()
    {
        if (dead)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            panel.SetActive(true);
        }
    }

}
