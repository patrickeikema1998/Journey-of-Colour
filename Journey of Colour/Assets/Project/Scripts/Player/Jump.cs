using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [HideInInspector] public bool jump, canJump;
    public float jumpForce;
    PlayerHealth health;
    SwapClass playerClass;
    Rigidbody rb;
    [SerializeField] AudioSource sound;
    Vector2 angelPitch, devilPitch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerClass = GetComponent<SwapClass>();
        health = GetComponent<PlayerHealth>();

        angelPitch = new Vector2(1.1f, 1.2f);
        devilPitch = new Vector2(0.9f, 1f);
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
            if (playerClass.IsAngel()) sound.pitch = Random.Range(angelPitch.x, angelPitch.y);
            else sound.pitch = Random.Range(devilPitch.x, devilPitch.y);
            sound.Play();
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
