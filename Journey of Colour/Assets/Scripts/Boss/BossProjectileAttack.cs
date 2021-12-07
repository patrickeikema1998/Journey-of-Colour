using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileAttack : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    SlimeBossController controller;

    [SerializeField]
    int timesShotBeforeBurst = 3,
        burstShotAmount = 3;
    [SerializeField]
    float burstTime = 0.1f;

    int timesShot;

    [System.NonSerialized]
    public float shootCooldown;

    public int bulletAmount = 8;

    [System.NonSerialized]
    public bool burstEnabled = false;

    float shootCooldownTimer = 0;

    // Update is called once per frame
    void Update()
    {
        shootCooldownTimer += Time.deltaTime;
        if (shootCooldownTimer >= shootCooldown)
        {
            if (timesShot >= timesShotBeforeBurst && burstEnabled)
            {
                shootCooldownTimer = 0 - burstTime * burstShotAmount;
                Burst();
                timesShot = 0;
            }
            else
            {
                shootCooldownTimer = 0;
                Shoot();
                timesShot++;
            }
        }
    }

    void Burst()
    {
        for (int i = 0; i < burstShotAmount; i++)
        {
            Invoke("Shoot", burstTime * i);
        }
        Invoke("Stun", burstTime * burstShotAmount);
    }

    void Shoot()
    {
        float degrees = 360 / bulletAmount;
        for (int i = 0; i < bulletAmount; i++) 
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, degrees * i)));
        }
    }

    void Stun()
    {
        controller.Stun();
    }
}