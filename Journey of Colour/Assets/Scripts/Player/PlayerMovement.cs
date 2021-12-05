using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    MeleeAttack meleeAttack;

    private Vector3 PlayerMovementInput;

    public float speed;
    public float jumpForce;

    //public bool isJumpButtonPressed = false;
    public bool isGrounded = false;
    public bool jump;

    public bool lookingLeft;

    string lastPressed;
    string currentPressed;

    public GameObject fireBall;
    private FireBall bulletScript;


    public void Start()
    {
        bulletScript = fireBall.GetComponent<FireBall>();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    public void Update()
    {
        if (!lookingLeft)
        {
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            bulletScript.speed = 20;
        }
        else
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            bulletScript.speed = -20;
        }

        lastPressed = currentPressed;

        if (lastPressed == "a") lookingLeft = true;
        else lookingLeft = false;

        if (Input.GetKeyDown("a")) currentPressed = "a";

        if (Input.GetKeyDown("d")) currentPressed = "d";

        if (Input.GetButtonDown("Fire1")) meleeAttack.Attack();


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }


    void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        GroundCheck();
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, 0.7f))
        {
            isGrounded = true;
        }
        else if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, 0.7f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
}
