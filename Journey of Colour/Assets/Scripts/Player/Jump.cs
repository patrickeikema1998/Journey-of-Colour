using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool jump;
    public bool canJump;
    public float jumpForce;
    PlayerHealth health;
    SwapClass playerClass;
    Rigidbody rb;
    Vector2 angelPitch, devilPitch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerClass = GetComponent<SwapClass>();
        health = GetComponent<PlayerHealth>();

        angelPitch = new Vector2(1.2f, 1.3f);
        devilPitch = new Vector2(1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            if (playerClass.IsAngel()) GetComponentInChildren<DoubleJump>().canDoubleJump = true;
        }

        if (jump && canJump && !health.dead)
        {
            if (playerClass.IsAngel()) AudioManager.instance.PlayOrStop("jump", true, angelPitch);
            else AudioManager.instance.PlayOrStop("jump", true, devilPitch);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            GetComponentInChildren<PlayerAnimations>().Jump();
        }
    }
    private void FixedUpdate()
    {
        JumpCheck();
    }

    void JumpCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, .85f) && !health.dead)
        {
            canJump = true;
        }
        else if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.down, out hit, .85f) && !health.dead) canJump = true;
        else canJump = false;

    }
}
