using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] float jumpForceTwo;
    bool isGrounded;
    bool isJumpButtonPressed;
    bool jumpedTwice;
    Rigidbody rb;

    SwapClass playerClass;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        jumpedTwice = false;

        playerClass = GetComponent<SwapClass>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.isGrounded = GetComponent<PlayerMovement>().isGrounded;
        this.isJumpButtonPressed = GetComponent<PlayerMovement>().isJumpButtonPressed;

        if (!isGrounded)
        {
            if (isJumpButtonPressed && !jumpedTwice && playerClass.playerClass == 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                rb.AddForce(Vector3.up * jumpForceTwo, ForceMode.Impulse);
                isJumpButtonPressed = false;
                jumpedTwice = true;
            }
        }
        else { jumpedTwice = false; }
    }
}
