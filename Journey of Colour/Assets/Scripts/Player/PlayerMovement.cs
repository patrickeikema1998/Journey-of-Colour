using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animatorAngel,animatorDevil;
    AnimationManager animationManager;
    public Rigidbody rb;
    MeleeAttack meleeAttack;

    private Vector3 PlayerMovementInput;

    float xAxis;
    public float speed;
    public float jumpForce;

    //public bool isJumpButtonPressed = false;
    public bool isGrounded = false;
    public bool jump;

    public bool lookingLeft;

    string lastPressed;
    string currentPressed;

    public GameObject fireBall;
    private FireBall bulletScript;

    public void Start()
    {
        bulletScript = fireBall.GetComponent<FireBall>();
        meleeAttack = GetComponent<MeleeAttack>();
        animatorAngel = GameObject.Find("Angel Player").GetComponent<Animator>();
        animatorDevil = GameObject.Find("Devil Player").GetComponent<Animator>();
        animationManager = new AnimationManager();
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


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }


    void FixedUpdate()
    {
        Movement();

        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        GroundCheck();

    }

    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, 0.7f))
        {
            isGrounded = true;
        }
        else if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, 0.7f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    private void Movement()
    {
        xAxis *= speed * Time.deltaTime;
        Debug.Log(xAxis);
        transform.position = new Vector3(transform.position.x + xAxis, transform.position.y, transform.position.z);

        if(xAxis != 0)
        {
            animationManager.changeAnimationState(animatorAngel, "character_run");
        } 
    }
}
