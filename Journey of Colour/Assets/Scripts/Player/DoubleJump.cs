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
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        jumpedTwice = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.isGrounded = GetComponent<PlayerMovement>().isGrounded;
        this.isJumpButtonPressed = GetComponent<PlayerMovement>().isJumpButtonPressed;

        if (!isGrounded)
        {
            if (isJumpButtonPressed && !jumpedTwice)
            {
                rb.AddForce(new Vector3(0, jumpForceTwo * Time.deltaTime, 0), ForceMode.Impulse);
                isJumpButtonPressed = false;
                jumpedTwice = true;
            }
        }
        else { jumpedTwice = false; }
    }
}
