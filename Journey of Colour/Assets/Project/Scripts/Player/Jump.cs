using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [HideInInspector] public bool jump, canJump;
    public float jumpForce;
    [SerializeField] float groundDistanceToJump = 1f;
    private float raycastSideCheck = 0.5f;
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

            // Set velocity to zero, so when we want to jump again, the player won't have a downwards velocity.
            rb.velocity = Vector3.zero;
            // Add a force to the rigidbody so it goes up.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            GetComponentInChildren<PlayerAnimations>().Jump();
        }
    }

    // In JumpCheck we are using Physics. So we want to use the FixedUpdate.
    private void FixedUpdate()
    {
        JumpCheck();
    }

    void JumpCheck()
    {
        // The player can jump if he is close to the ground. He doesn't need to collide with it.
        // We do this for the gamefeel. The player can now jump a little before he hits the ground.
        // We have 2 raycasts so that when the player is next to a cliff (but his center of gravity is above the cliff), the player can still jump.
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x - raycastSideCheck, transform.position.y, transform.position.z), Vector3.down, out hit, groundDistanceToJump) && !health.dead)
            canJump = true;
        else if (Physics.Raycast(new Vector3(transform.position.x + raycastSideCheck, transform.position.y, transform.position.z), Vector3.down, out hit, groundDistanceToJump) && !health.dead)
            canJump = true;
        else canJump = false;
    }
}
