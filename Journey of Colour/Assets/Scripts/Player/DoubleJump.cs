using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] float jumpForceTwo;
    bool isGrounded;
    bool isJumpButtonPressed;
    bool jumpedTwice;
    public bool canDoubleJump, doubleJump;
    Rigidbody rb;
    SwapClass swapClass;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        jumpedTwice = false;
        swapClass = GetComponent<SwapClass>();
    }

    private void Update()
    {
        if (swapClass.currentClass == SwapClass.playerClasses.Angel && Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !GetComponent<PlayerMovement>().canJump) doubleJump = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (doubleJump)
        {

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForceTwo, ForceMode.Impulse);
            canDoubleJump = false;
            doubleJump = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") canDoubleJump = false;
    }
}
