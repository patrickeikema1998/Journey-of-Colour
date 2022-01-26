using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    CustomTimer cooldownTimer;
    [SerializeField] float cooldownInSeconds, InitialDashRange, lerpSpeed;
    float wantedPosX;
    float stopLerpingRange = .5f;
    bool dash;
    Rigidbody rb;
    PlayerMovement movement;
    Float floatAbility;
    PlayerAnimations anim;
    GameObject player;
    TrailRenderer trail;
    [SerializeField] AudioSource sound;

    Vector3 rightRotation = new Vector3(0f, 90f, 0f);
    Vector3 leftRotation = new Vector3(0f, 270f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = new CustomTimer(cooldownInSeconds);
        cooldownTimer.finish = true;

        player = transform.parent.gameObject;
        trail = GetComponent<TrailRenderer>();
        movement = player.GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnimations>();
        floatAbility = GetComponent<Float>();
        trail.enabled = false;

        stopLerpingRange = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer.Update();
        if(dash) DoDash();

        if (Input.GetKeyDown(GameManager.GM.dashAbility) && cooldownTimer.finish && !floatAbility.isFloating)
        {
            //animation and sound
            sound.Play();
            anim.DashAnimation();

            //start trail
            trail.enabled = true;

            //constrains
            movement.canMove = false;
            movement.canTurn = false;
            rb.useGravity = false;

            //changes direction based on rotation of the player
            if (transform.parent.rotation.eulerAngles == rightRotation) wantedPosX = transform.parent.position.x + InitialDashRange;
            else if(transform.parent.rotation.eulerAngles == leftRotation) wantedPosX = transform.parent.position.x - InitialDashRange;

            //starts the dash and resets the timer.
            dash = true;
            cooldownTimer.Reset();
        }
    }

    void DoDash()
    {
        
        rb.velocity = Vector3.zero;

        //the actual movement of the dash.
        Vector3 pos = transform.parent.position;
        transform.parent.position = new Vector3(Mathf.Lerp(pos.x, wantedPosX, lerpSpeed * Time.deltaTime), pos.y, pos.z);

        //checks if dash is almost there, then stops it.
        if (Mathf.Abs(pos.x - wantedPosX) < stopLerpingRange)
        {
            dash = false;
            movement.canTurn = true;
            movement.canMove = true;
            rb.useGravity = true;
            trail.enabled = false;
        }    
    }
}
