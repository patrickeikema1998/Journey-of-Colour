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

    public bool isGrounded = false;
    public bool jump;

    public bool lookingLeft;

    string lastPressed;
    string currentPressed;

    public void Start()
    {
        meleeAttack = GetComponent<MeleeAttack>();
    }

    public void Update()
    {
        /*if (!lookingLeft)
        {
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            
        }
        else
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        lastPressed = currentPressed;

        //if (lastPressed == "a") lookingLeft = true;
        //else lookingLeft = false;

        //if (Input.GetKeyDown("a")) currentPressed = "a";

        //if (Input.GetKeyDown("d")) currentPressed = "d";

        if (Input.GetButtonDown("Fire1")) meleeAttack.Attack();
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
            jump = false;
        }
    }

    void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
    }
}
