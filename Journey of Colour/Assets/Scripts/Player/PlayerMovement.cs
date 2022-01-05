using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool jump;
    NewPlayerAnimations playerAnim;
    [HideInInspector]public Rigidbody rb;
    MeleeAttack meleeAttack;
    CustomTimer meleeAttackCooldownTimer;
    [SerializeField] float meleeAttackCDInSeconds;
    [HideInInspector] public SwapClass playerClass;

    private Vector3 PlayerMovementInput;

    [HideInInspector] public float xAxis;
    public float movementSpeedAngel, movementSpeedDevil;
    public float jumpForce;

    //public bool isJumpButtonPressed = false;
    public bool canJump = false;
    public bool lookingLeft;
    public bool isGrounded;
    public bool canMove;
    public bool canTurn;

    string lastPressed;
    string currentPressed;

    public GameObject fireBall;
    private FireBall bulletScript;
    Health playerHealth;
    public NewPlayerAnimations PlayerAnim
    {
        get { return playerAnim; }
        set
        {
            if (value == playerAnim) return;
            playerAnim = value;
        }
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<Health>();
        canTurn = true;
        canMove = true;
        meleeAttackCooldownTimer = new CustomTimer(meleeAttackCDInSeconds);
        meleeAttackCooldownTimer.start = true;
        playerClass = GetComponent<SwapClass>();
        bulletScript = fireBall.GetComponent<FireBall>();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    public void Update()
    {
        if (playerClass.IsAngel()) PlayerAnim = GameObject.Find("Angel Player").GetComponent<NewPlayerAnimations>();
        else PlayerAnim = GameObject.Find("Devil Player").GetComponent<NewPlayerAnimations>();


        meleeAttackCooldownTimer.Update();

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

        if (playerClass.IsDevil() && Input.GetButtonDown("Fire1") && meleeAttackCooldownTimer.finish && !playerHealth.dead)
        {
            meleeAttack.Attack();
            meleeAttackCooldownTimer.Reset();
            playerAnim.Attack();
        }


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
        HandleRotation();

        if(jump && canJump && !playerHealth.dead)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            playerAnim.Jump();
        }
    }

    void JumpCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, .85f) && !playerHealth.dead)
        {
            canJump = true;
        }
        else if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, .85f) && !playerHealth.dead) canJump = true;
        else canJump = false;

    }

    private void Movement()
    {
        float movementSpeed;
        if (playerClass.IsAngel()) movementSpeed = movementSpeedAngel;
        else movementSpeed = movementSpeedDevil;
        xAxis *= movementSpeed * Time.deltaTime;

        if (canMove && !playerHealth.dead)
        {
            transform.position = new Vector3(transform.position.x + xAxis, transform.position.y, transform.position.z);
        }
    }

    private void HandleRotation()
    {
        if (!GetComponent<Float>().isFloating && canTurn && !playerHealth.dead)
        {
            if (Input.GetAxis("Horizontal") < 0) transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            else if (Input.GetAxis("Horizontal") > 0) transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
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
