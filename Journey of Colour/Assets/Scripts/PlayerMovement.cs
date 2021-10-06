using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce = 2.0f;
    private bool isGrounded;
    Rigidbody rb;
    bool jump;
    private Animator anim;
    SpriteRenderer renderer;
    float airY;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xDir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(xDir * speed, rb.velocity.y, rb.velocity.z);



    }
    private void Update()
    {
        Jump();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            renderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            renderer.flipX = false;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;

    }

    protected void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }


}
