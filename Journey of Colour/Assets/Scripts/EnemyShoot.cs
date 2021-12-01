using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 5f, offsetDistance;
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
        Vector3 dir = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        Instantiate(bullet, (transform.position + (dir * offsetDistance)), Quaternion.identity);
    }
}
