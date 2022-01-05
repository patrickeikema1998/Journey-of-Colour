using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    public GameObject fireBallPoint;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                Invoke("Shoot", shootAnimationTime);
                playerMovement.PlayerAnim.RangeAttack();
            }
            shootTimer.Reset();
            shootTimer.start = true;
        }
    }

    void Shoot()
    {
        //instantiate a bullet on a certain position (the bulletPoint).
        Instantiate(fireBallPrefab, fireBallPoint.transform.position, fireBallPoint.transform.rotation);
    }
}