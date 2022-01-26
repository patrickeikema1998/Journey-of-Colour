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
    private float gravityTime;
    private GameObject player;
    private Rigidbody rb;
    private SwapClass swapClass;
    private PlayerAnimations playerAnim;
    private PlayerHealth playerHealth;
    private PlayerMovement movement;
    private Float _float;

    // Start is called before the first frame update
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
        gravityTime = - 0.4f;
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
                if (duration < gravityTime) rb.useGravity = true;
            }
        }

    }

    //The actual dash function is called every frame when duration is more than zero
    public void Dash()
    {
        rb.velocity = new Vector3(rb.velocity.x,0,0);
        rb.AddForce(new Vector3(direction*dashForce,0,0));
        coolDown = coolDownTime;
    }
    
}
