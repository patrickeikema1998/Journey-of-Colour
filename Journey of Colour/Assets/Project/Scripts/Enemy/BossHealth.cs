using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    [SerializeField] int bossHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = bossHealth;
        health = bossHealth;
        slider = GetComponentInChildren<Slider>();
    }
}
