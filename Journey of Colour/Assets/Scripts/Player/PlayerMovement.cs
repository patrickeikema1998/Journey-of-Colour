using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    MeleeAttack meleeAttack;

    //public float sideForce;
    private Vector3 PlayerMovementInput;

    public float speed;
    public float jumpForce;

    public bool isJumpButtonPressed = false;
    public bool isGrounded = false;

    void Start()
    {
        meleeAttack = GetComponent<MeleeAttack>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
        }
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
