using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject player;
    [SerializeField] float fireRate = 5f, offsetFloat, enemySight;
    

    float coolDown;
    
    Vector3 offset;
    
    bool lookingLeft;

    float distance;

    void Start()
    {
        coolDown = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(offsetFloat, 0, 0);

        coolDown -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, player.transform.position);

        if (coolDown < 0 && distance < enemySight)
        {
            FireBullet();
        }

        if (transform.position.x < player.transform.position.x)
            lookingLeft = true; 
        else
            lookingLeft = false;

        if(lookingLeft)
            offsetFloat = 1;
        else
            offsetFloat = -1;
    }

    void FireBullet()
    {
        coolDown = fireRate;
        Instantiate(bullet, transform.position + offset, Quaternion.identity);
    }
}
