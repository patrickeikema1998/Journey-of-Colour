using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform fireBallPointLeft;
    public Transform fireBallPointRight;
    public GameObject fireBallPrefab;
    public float shootTime;
    private FireBall bullet;
    public GameObject fireBall;
    private FireBall bulletScript;
    PlayerHealth playerHealth;
    [SerializeField] AudioSource fireSound;


    [HideInInspector] public float cdTime;
    GameObject player;
    SwapClass swapClass;
    PlayerMovement playerMovement;
    PlayerAnimations anim;
    CustomTimer shootTimer;

    float shootAnimationTime = 0.4f;

    private void Start()
    {
        cdTime = shootTime;
        player = transform.parent.gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        swapClass = player.GetComponent<SwapClass>();
        playerMovement = player.GetComponent<PlayerMovement>();
        shootTimer = new CustomTimer(shootTime);
        shootTimer.start = true;
        shootTimer.finish = true;
        bullet = fireBall.GetComponent<FireBall>();
        anim = GetComponent<PlayerAnimations>();
        bulletScript = fireBall.GetComponent<FireBall>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer.Update();

        //player can only fire a fireball when he is in class 1. also known as the black colour.
        
        if (Input.GetKeyDown(GameManager.GM.fireBallAbility) && shootTimer.finish && !playerHealth.dead)
        {
            anim.RangeAttackAnimation();
            fireSound.Play();
            if (Input.mousePosition.x > player.transform.position.x)
            {
                Invoke("ShootRight", shootAnimationTime);
            }
            else if (Input.mousePosition.x < player.transform.position.x)
            {
                Invoke("ShootLeft", shootAnimationTime);

            }
            shootTimer.Reset();
            shootTimer.start = true;
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
