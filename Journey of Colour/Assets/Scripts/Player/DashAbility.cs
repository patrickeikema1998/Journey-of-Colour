using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{

    [SerializeField] float dashForce = 80;
    [SerializeField] float coolDownTime = 0.5f;
    [SerializeField] float durationTime = 0.2f;

    private float direction;
    private float coolDown;
    private float duration;
    GameObject player;
    Rigidbody rb;
    SwapClass swapClass;
    PlayerAnimations playerAnim;
    PlayerHealth playerHealth;
    PlayerMovement movement;
    Float _float;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        _float = GetComponent<Float>();
        playerHealth = player.GetComponent<PlayerHealth>();
        movement = player.GetComponent<PlayerMovement>();
        playerAnim = GetComponent<PlayerAnimations>();
        coolDown = coolDownTime;
        swapClass = player.GetComponent<SwapClass>();
        duration = durationTime;
        direction = 1;
    }
    // Update is called once per frame
    void Update()
    {
        //countdown for timers
        coolDown -= Time.deltaTime;
        duration -= Time.deltaTime;

        //checks the last direction the player is facing
        if (Input.GetAxis("Horizontal") < 0 && movement.canMove == true) direction = -1;
        if (Input.GetAxis("Horizontal") > 0 && movement.canMove == true) direction = 1;

        //Checks if player is able to dash
        if (Input.GetKeyDown(GameManager.GM.dashAbility) && coolDown < 0 && !playerHealth.dead && !_float.isFloating)
        {
            playerAnim.Dash();
            rb.velocity = Vector3.zero;
            duration = durationTime;
        }

        //Dashes only when the duration is higher than zero, so that you can decide the duration of the dash
        if (!_float.isFloating)
        {
            if (duration > 0)
            {
                rb.useGravity = false;
                movement.canMove = false;
                Dash();
            }
            else
            {
                movement.canMove = true;
                if (duration < -0.4) rb.useGravity = true;
            }
        }

    }


    public void Dash()
    {
        AudioManager.instance.PlayOrStop("dash", true);
        rb.velocity = new Vector3(rb.velocity.x,0,0);
        rb.AddForce(new Vector3(direction*dashForce,0,0));
        coolDown = coolDownTime;
    }
    
}
