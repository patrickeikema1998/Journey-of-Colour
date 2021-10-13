using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //instantiate a bullet on a certain position (the bulletPoint).
        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }
}
