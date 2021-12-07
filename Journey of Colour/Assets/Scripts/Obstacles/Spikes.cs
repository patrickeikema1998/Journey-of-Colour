using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //necessary objects
    Rigidbody rb;
    [SerializeField] Rigidbody rbPlayer;

    //timer stuff
    CustomTimer holdTimer;
    [SerializeField] float holdTime;

    //distance variables
    [SerializeField] float range;
    float distanceToPlayer;

    [SerializeField] float retractedPosY, extractedPosY, moveSpeed;
    bool extracted;

    // Start is called before the first frame update
    void Start()
    {
        extracted = true;
        rb = GetComponent<Rigidbody>();
        holdTimer = new CustomTimer(holdTime);
        retractedPosY = rb.position.y + retractedPosY;
        extractedPosY = rb.position.y + extractedPosY;
    }

    // Update is called once per frame
    void Update()
    {
        holdTimer.Update();
        StartTimerIfInRange();
        Animate();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Spike hit");

        //deal damage to object
        if (other.GetComponent<Health>() != null)
        {
           other.GetComponent<Health>().Damage(4);
           other.GetComponent<TakeDamage>().TakeHit(40);
        }

    }

    void StartTimerIfInRange()
    {
        distanceToPlayer = Mathf.Abs(rb.position.x - rbPlayer.position.x);

        //checks if player is in range, stops the spikes from updating if not
        if (distanceToPlayer < range)
        {
            holdTimer.start = true;        }
        else
        {
            holdTimer.start = false;
        }
    }

    private void Animate()
    {
        //if the hold time is finished, checks if the spiked are extracted.
        if (holdTimer.finish)
        {
            if (extracted)
            {
                //if the posY is higher than the 
                if (rb.position.y > retractedPosY)
                {
                    rb.velocity = new Vector3(rb.velocity.x, -moveSpeed, rb.velocity.z);
                }
                else
                {
                    rb.position = new Vector3(rb.position.x, retractedPosY, rb.position.z);
                    rb.velocity = Vector3.zero;
                    extracted = false;
                    holdTimer.Reset();
                }
            } else
            {
                if(rb.position.y < extractedPosY)
                {
                    rb.velocity = new Vector3(rb.velocity.x, moveSpeed, rb.velocity.z);
                } else
                {
                    rb.position = new Vector3(rb.position.x, extractedPosY, rb.position.z);
                    rb.velocity = Vector3.zero;
                    extracted = true;
                    holdTimer.Reset();
                }
            }
        }
    }
}
