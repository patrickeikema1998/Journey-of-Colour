using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    private Vector3 PlayerMovementInput;

    public float speed;
    public float jumpForce;

    public bool isJumpButtonPressed = false;
    public bool isGrounded = false;

    public bool lookingLeft;

    public string lastPressed;
    string currentPressed;

    public GameObject bullet;
    private Bullet bulletScript;

    public void Start()
    {
        bulletScript = bullet.GetComponent<Bullet>();
    }

    public void Update()
    {
        if (!lookingLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);
            bulletScript.speed = 20;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            bulletScript.speed = -20;
        }
        if (Input.GetButtonDown("Jump")) 
            isJumpButtonPressed = true;

        lastPressed = currentPressed;

        if (lastPressed == "a") lookingLeft = true;
        else lookingLeft = false;

        if (Input.GetKeyDown("a")) currentPressed = "a";

        if (Input.GetKeyDown("d")) currentPressed = "d";
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumpButtonPressed = false;
        }
    }

    void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (isJumpButtonPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpButtonPressed = false;
        }
    }
}
