using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform fireBallPointLeft;
    public Transform fireBallPointRight;
    public GameObject fireBallPrefab;

    SwapClass swapClass;
    PlayerMovement playerMovement;

    private void Start()
    {
        swapClass = GetComponent<SwapClass>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //player can only fire a fireball when he is in class 1. also known as the black colour.
        if (swapClass.playerClass == 1)
        {

            if (Input.GetKeyDown("z"))
            {
                if (!playerMovement.lookingLeft)
                {
                    ShootRight();
                }
                else if (playerMovement.lookingLeft)
                {
                    ShootLeft();
                }
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
