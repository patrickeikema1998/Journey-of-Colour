using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float dashForce = 80;
    [SerializeField] float coolDownTime = 0.5f;
    [SerializeField] float durationTime = 0.2f;

    private float direction;
    private float coolDown;
    private float duration;
    SwapClass swapClass;
    PlayerAnimations playerAnim;
    Health playerHealth;
    PlayerMovement movement;
    Float _float;

    void Start()
    {
        _float = GetComponent<Float>();
        playerHealth = GetComponent<Health>();
        movement = GetComponent<PlayerMovement>();
        playerAnim = GameObject.Find("Angel Player").GetComponent<PlayerAnimations>();
        coolDown = coolDownTime;
        swapClass = GetComponent<SwapClass>();
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
        if (Input.GetMouseButtonDown(0) && coolDown < 0 && swapClass.IsAngel() && !playerHealth.dead && !_float.isFloating)
        {
            playerAnim.Dash();
            rb.velocity = Vector3.zero;
            duration = durationTime;
        }
        //Dashes only when the duration is higher than zero, so that you can decide the duration of the dash
        if (duration > 0)
        {
            rb.useGravity = false;
            movement.canMove = false;
            Dash();
        }
        else
        {
            movement.canMove = true;
            if (duration<-0.4) rb.useGravity = true;
        }
    }


    public void Dash()
    {
        rb.velocity = new Vector3(rb.velocity.x,0,0);
        rb.AddForce(new Vector3(direction*dashForce,0,0));
        coolDown = coolDownTime;
    }
    
}
