using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform fireBallPointLeft;
    public Transform fireBallPointRight;
    public GameObject fireBallPrefab;
    public float shootTime;

    SwapClass swapClass;
    PlayerMovement playerMovement;
    CustomTimer shootTimer;

    private void Start()
    {
        swapClass = GetComponent<SwapClass>();
        playerMovement = GetComponent<PlayerMovement>();
        shootTimer = new CustomTimer(shootTime);
        shootTimer.start = true;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer.Update();

        //player can only fire a fireball when he is in class 1. also known as the black colour.
        if (swapClass.playerClass == 1)
        {
            if (Input.GetMouseButtonDown(1) && shootTimer.finish == true)
            {
                if (!playerMovement.lookingLeft)
                {
                    ShootRight();
                }
                else if (playerMovement.lookingLeft)
                {
                    ShootLeft();
                }
                shootTimer.Reset();
                shootTimer.start = true;
            }
        }
    }

    void ShootRight()
    {
        //instantiate a bullet on a certain position (the bulletPoint).
        Instantiate(fireBallPrefab, fireBallPointRight.position, fireBallPointRight.rotation);
    }

    void ShootLeft()
    {
        //instantiate a bullet on a certain position (the bulletPoint).
        Instantiate(fireBallPrefab, fireBallPointLeft.position, fireBallPointLeft.rotation);
    }
}
