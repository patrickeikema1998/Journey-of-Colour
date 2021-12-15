using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool jump;
    PlayerAnimations playerAnim;
    public Rigidbody rb;
    MeleeAttack meleeAttack;
    SwapClass playerClass;

    private Vector3 PlayerMovementInput;

    public float xAxis;
    public float speedAngel, speedDevil;
    public float jumpForce;

    //public bool isJumpButtonPressed = false;
    public bool canJump = false;
    public bool lookingLeft;
    public bool isGrounded;

    string lastPressed;
    string currentPressed;

    public GameObject fireBall;
    private FireBall bulletScript;

    public void Start()
    {
        playerClass = GetComponent<SwapClass>();
        bulletScript = fireBall.GetComponent<FireBall>();
        meleeAttack = GetComponent<MeleeAttack>();
        playerAnim = GetComponent<PlayerAnimations>();
    }

    public void Update()
    {
        xAxis = Input.GetAxis("Horizontal");

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

        lastPressed = currentPressed;

        if (lastPressed == "a") lookingLeft = true;
        else lookingLeft = false;

        if (Input.GetKeyDown("a")) currentPressed = "a";

        if (Input.GetKeyDown("d")) currentPressed = "d";

        if (Input.GetButtonDown("Fire1")) meleeAttack.Attack();


        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            if(playerClass.IsAngel()) GetComponent<DoubleJump>().canDoubleJump = true;
        }
    }


    void FixedUpdate()
    {
        Movement();
        JumpCheck();
        RotateCharacter();

        if(jump && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
    }

    void JumpCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, .85f))
        {
            canJump = true;
        }
        else if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, .85f)) canJump = true;
        else canJump = false;

    }

    private void Movement()
    {
        float speed;
        if (playerClass.IsAngel()) speed = speedAngel;
        else speed = speedDevil;

        xAxis *= speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xAxis, transform.position.y, transform.position.z);
    }

    private void RotateCharacter()
    {
        if (Input.GetAxis("Horizontal") < 0) transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        else if (Input.GetAxis("Horizontal") > 0) transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Ground") isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }
}
