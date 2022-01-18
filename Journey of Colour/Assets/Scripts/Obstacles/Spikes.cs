using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //necessary objects
    Rigidbody rb;
    //timer stuff
    CustomTimer holdTimer, damageTimer;
    [SerializeField] float holdTimeSpikes, timeBetweenDamage, moveSpeed;
    [SerializeField] int damage;
    [SerializeField] AudioSource soundExtract, soundRetract;
    bool soundPlaying;
    float retractedPosY, extractedPosY;
    bool extracted;
    [SerializeField] float retractDistance;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        extractedPosY = rb.position.y;
        retractedPosY = rb.position.y - retractDistance;
        extracted = true;
        holdTimer = new CustomTimer(holdTimeSpikes);
        damageTimer = new CustomTimer(timeBetweenDamage);
        holdTimer.start = true;
        damageTimer.start = true;
    }

    // Update is called once per frame
    void Update()
    {
        holdTimer.Update();
        damageTimer.Update();
        ExtractRetract();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.GetComponent<PlayerHealth>() != null && damageTimer.finish)
            {
                other.GetComponent<PlayerHealth>().Damage(damage);
                damageTimer.Reset();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            damageTimer.Reset();
        }
    }



    private void ExtractRetract()
    {
        //if the hold time is finished, checks if the spiked are extracted.
        if (holdTimer.finish)
        {
            if (extracted)
            {
                if (!soundPlaying)
                {
                    soundRetract.Play();
                    soundPlaying = true;
                }

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
                    soundPlaying = false;
                    holdTimer.Reset();
                }
            } else
            {
                if (!soundPlaying)
                {
                    soundExtract.Play();
                    soundPlaying = true;
                }

                if (rb.position.y < extractedPosY)
                {
                    rb.velocity = new Vector3(rb.velocity.x, moveSpeed, rb.velocity.z);
                } else
                {
                    rb.position = new Vector3(rb.position.x, extractedPosY, rb.position.z);
                    rb.velocity = Vector3.zero;
                    extracted = true;
                    soundPlaying = false;
                    holdTimer.Reset();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SlimeBossController slimeBoss = other.GetComponent<SlimeBossController>();
        if (slimeBoss != null) slimeBoss.SpikesHit();
    }
}
