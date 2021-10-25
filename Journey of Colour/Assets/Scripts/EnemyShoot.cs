using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 5f;
    float coolDown;
   
    void Start()
    {
        coolDown = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;

        if (coolDown < 0)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        coolDown = fireRate;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
