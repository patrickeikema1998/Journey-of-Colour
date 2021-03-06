using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] float jumpForceTwo;
    bool isGrounded;
    bool isJumpButtonPressed;
    bool jumpedTwice;
    [HideInInspector] public bool canDoubleJump, doubleJump;
    GameObject player;
    Rigidbody rb;
    PlayerMovement playerMovement;
    SwapClass swapClass;
    PlayerAnimations playerAnim;
    Jump jump;
    [SerializeField] AudioSource jumpSound;
    Vector2 pitch;
    [HideInInspector] public bool landed;

    // Start is called before the first frame update
    void Start()
    {
        landed = true;
        player = GameObject.Find("Player");
        jump = player.GetComponentInChildren<Jump>();
        playerAnim = GetComponent<PlayerAnimations>();
        rb = player.GetComponent<Rigidbody>();
        playerMovement = player.GetComponent<PlayerMovement>();
        swapClass = player.GetComponent<SwapClass>();
        jumpedTwice = false;
        pitch = new Vector2(1.2f, 1.3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !jump.canJump) doubleJump = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (doubleJump)
        {
            jumpSound.pitch = Random.Range(pitch.x, pitch.y);
            jumpSound.Play();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForceTwo, ForceMode.Impulse);
            canDoubleJump = false;
            doubleJump = false;

            landed = false;
            playerAnim.DoubleJump();
        }

        if (jump.canJump) { landed = true; }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") canDoubleJump = false;
    }
}
