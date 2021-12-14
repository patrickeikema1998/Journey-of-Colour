using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject player;
    [SerializeField] float fireRate = 5f, enemySight, offsetFloatY;
    float offsetFloat;
    EnemyAnimations anim;
    [SerializeField] bool spearThrower;

    float coolDown;
    
    Vector3 offset;
    
    bool lookingLeft;

    float distance;

    void Start()
    {
        anim = GetComponent<EnemyAnimations>();
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
        float attackAnimationLength = .55f;
        anim.DoAttackAnimation();
        coolDown = fireRate;
        Invoke("InstantiateBullet", attackAnimationLength);
    }

    void InstantiateBullet()
    {
        if (spearThrower)
        {
            Instantiate(bullet, transform.position + offset, bullet.transform.rotation);

        } else
        {
            Instantiate(bullet, transform.position + offset, Quaternion.identity);
        }
    }
}
