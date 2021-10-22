using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform fireBallPoint;
    public GameObject fireBallPrefab;

    SwapClass swapClass;

    private void Start()
    {
        swapClass = GetComponent<SwapClass>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swapClass.playerClass == 1)
            if (Input.GetKeyDown("z"))
            {
                Shoot();
            }
    }

    void Shoot()
    {
        //instantiate a bullet on a certain position (the bulletPoint).

        Instantiate(fireBallPrefab, fireBallPoint.position, fireBallPoint.rotation);
    }
}
