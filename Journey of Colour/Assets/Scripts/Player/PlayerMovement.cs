using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float sideForce;
    public float jumpForce;

    public bool isJumpButtonPressed = false;
    public bool isGrounded = false;

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
        }
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
        var XMovement = Input.GetAxis("Horizontal");
        rb.AddForce(sideForce * XMovement * Time.deltaTime, 0, 0);

        if (isJumpButtonPressed && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Impulse);
            isJumpButtonPressed = false;
        }
    }
}
