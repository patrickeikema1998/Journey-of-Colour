using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    CustomTimer cooldownTimer, trailTimer;
    [SerializeField] float cooldownInSeconds, trailTime, InitialDashRange, lerpSpeed;
    float wantedPosX, stopDashRange, dashRangeLeft, dashRangeRight;
    bool dash;
    Rigidbody rb;
    PlayerMovement movement;
    Float floatAbility;
    PlayerAnimations anim;
    GameObject player;
    TrailRenderer trail;
    [SerializeField] AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = new CustomTimer(cooldownInSeconds);
        cooldownTimer.finish = true;
        trailTimer = new CustomTimer(trailTime);


        player = transform.parent.gameObject;
        trail = GetComponent<TrailRenderer>();
        movement = player.GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnimations>();
        floatAbility = GetComponent<Float>();
        trail.enabled = false;

        stopDashRange = 1f;
        dashRangeLeft = -InitialDashRange;
        dashRangeRight = InitialDashRange;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer.Update();
        trailTimer.Update();

        if(Input.GetKeyDown(KeyCode.E) && cooldownTimer.finish && !floatAbility.isFloating)
        {
            //animation and sound
            sound.Play();
            anim.Dash();

            //start timers
            trailTimer.start = true;
            trail.enabled = true;

            //constrains
            movement.canMove = false;
            movement.canTurn = false;
            rb.useGravity = false;

            //changes direction based on rotation
            if (transform.parent.rotation.eulerAngles.y == 270) wantedPosX = transform.parent.position.x + dashRangeLeft;
            else wantedPosX = transform.parent.position.x + dashRangeRight;

            //starts the dash and resets the timer.
            dash = true;
            cooldownTimer.Reset();
        }

        DoDash();


        if (trailTimer.finish)
        {
            trailTimer.Reset();
            trailTimer.start = false;
            trail.enabled = false;
        }
    }


    void DoDash()
    {
        if (dash)
        {
            rb.velocity = Vector3.zero;

            //the actual dash.
            Vector3 pos = transform.parent.position;
            transform.parent.position = new Vector3(Mathf.Lerp(pos.x, wantedPosX, lerpSpeed * Time.deltaTime), pos.y, pos.z);

            //checks if dash is almost there, then stops it.
            if (Mathf.Abs(pos.x - wantedPosX) < stopDashRange)
            {
                dash = false;
                movement.canTurn = true;
                movement.canMove = true;
                rb.useGravity = true;
            }
        }
    }
}
