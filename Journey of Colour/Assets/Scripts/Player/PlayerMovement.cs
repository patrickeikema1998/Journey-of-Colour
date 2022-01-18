using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerAnimations playerAnim;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SwapClass playerClass;
    private Vector3 PlayerMovementInput;

    [HideInInspector] public float xAxis;
    public float movementSpeedAngel, movementSpeedDevil;

    //public bool isJumpButtonPressed = false;
    public bool isGrounded;
    public bool canMove;
    public bool canTurn;

    string lastPressed;
    PlayerHealth playerHealth;
    [SerializeField] LayerMask layerMask;
    [SerializeField] BoxCollider playerCollider;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.0f;
        playerHealth = GetComponent<PlayerHealth>();
        canTurn = true;
        canMove = true;
        playerClass = GetComponent<SwapClass>();
    }

    public void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
    }


    void FixedUpdate()
    {
        Movement();
        HandleRotation();
        // StopStickingWall();
    }

    private void Movement()
    {
        float movementSpeed;
        if (playerClass.IsAngel()) movementSpeed = movementSpeedAngel;
        else movementSpeed = movementSpeedDevil;
        xAxis *= movementSpeed * Time.deltaTime;

        if (canMove && !playerHealth.dead)
        {
            rb.velocity = new Vector3(xAxis, rb.velocity.y, rb.velocity.z);
            //transform.position = new Vector3(transform.position.x + xAxis, transform.position.y, transform.position.z);
        }
    }

    void StopStickingWall()
    {

        if (xAxis != 0 && (Physics.Raycast(transform.position, Vector3.right, 0.01f, layerMask) || Physics.Raycast(transform.position, Vector3.left, 0.01f, layerMask)))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
    }

    private void HandleRotation()
    {
        if (canTurn && !playerHealth.dead)
        {
            if (Input.GetAxis("Horizontal") < 0) transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            else if (Input.GetAxis("Horizontal") > 0) transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }
}
