using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    public Transform fireBallPointLeft;
    public Transform fireBallPointRight;
    public GameObject fireBallPrefab;
    public float shootTime;
    private FireBall bullet;
    public GameObject fireBall;
    Health playerHealth;

    SwapClass swapClass;
    PlayerMovement playerMovement;
    CustomTimer shootTimer;

    float shootAnimationTime = 0.4f;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
        swapClass = GetComponent<SwapClass>();
        playerMovement = GetComponent<PlayerMovement>();
        shootTimer = new CustomTimer(shootTime);
        shootTimer.start = true;
        shootTimer.finish = true;
        bullet = fireBall.GetComponent<FireBall>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer.Update();

        //player can only fire a fireball when he is in class 1. also known as the black colour.
        if (swapClass.currentClass == SwapClass.playerClasses.Devil && !playerHealth.dead)
        {
            if (Input.GetMouseButtonDown(1) && shootTimer.finish == true)
            {
                playerMovement.PlayerAnim.RangeAttack();

                if (Input.mousePosition.x > playerPos.position.x)
                {
                    Invoke("ShootRight", shootAnimationTime);
                }
                else if (Input.mousePosition.x < playerPos.position.x)
                {
                    Invoke("ShootLeft", shootAnimationTime);

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
        bullet.speed = 20;
    }

    void ShootLeft()
    {
        //instantiate a bullet on a certain position (the bulletPoint).
        Instantiate(fireBallPrefab, fireBallPointLeft.position, fireBallPointLeft.rotation);
        bullet.speed = -20;
    }
}
