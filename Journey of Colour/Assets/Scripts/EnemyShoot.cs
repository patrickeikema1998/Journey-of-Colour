using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject player;
    [SerializeField] float fireRate = 5f, enemySight, offsetFloatY;
    float offsetFloat;
    NewEnemyAnimations anim;
    [SerializeField] bool spearThrower;

    float coolDown;
    
    Vector3 offset;
    
    bool lookingLeft;

    float distance;

    void Start()
    {
        anim = GetComponent<NewEnemyAnimations>();
        coolDown = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(offsetFloat, offsetFloatY, 0);

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
        anim.Attack();
        coolDown = fireRate;
        Invoke("InstantiateBullet", anim.attackAnimTime);
    }

    void InstantiateBullet()
    {
        if (spearThrower)
        {
            Instantiate(bullet, transform.position + offset, bullet.transform.rotation);

            //this gives the variables given in the inspector to the spear
            SpearPattern pattern = bullet.GetComponent<SpearPattern>();
            ThrowForceSpear throwForce = GetComponent<ThrowForceSpear>();
            pattern.damage = throwForce.damage;
            pattern.verticalForce = throwForce.verticalForce;
            pattern.horizontalForce = throwForce.horizontalForce;
        }
        else Instantiate(bullet, transform.position + offset, Quaternion.identity);
    }
}
