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

    public bool isJumpButtonPressed = false;
    public bool isGrounded = false;

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
        if (Input.GetKeyDown(KeyCode.Space))
            isJumpButtonPressed = true;

        lastPressed = currentPressed;

        if (lastPressed == "a") lookingLeft = true;
        else lookingLeft = false;

        if (Input.GetKeyDown("a")) currentPressed = "a";

        if (Input.GetKeyDown("d")) currentPressed = "d";

        if (Input.GetButtonDown("Fire1")) meleeAttack.Attack();
    }

    void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (isJumpButtonPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpButtonPressed = false;
        }
    }

    private bool IsGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }
}
