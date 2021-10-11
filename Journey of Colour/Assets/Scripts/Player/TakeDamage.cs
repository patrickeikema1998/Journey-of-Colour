using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar healthbar;
    public Rigidbody rb;
    public GameObject panel;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //If enemy attacks player, then the player takes the hit
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeHit(20);
        }

        if(currentHealth <= 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            panel.SetActive(true);
        }
    }

    void TakeHit(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }

}
