using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorYMovingPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float maxMovement, speed, waitTime;
    float movedDist;
    CustomTimer waitTimer;

    public bool vertical;
    float wantedPosX, wantedPosY;


    // Start is called before the first frame update
    void Start()
    {
        waitTimer = new CustomTimer(waitTime);
        waitTimer.start = true;

        if (vertical)
        {
            wantedPosY = rb.transform.position.y;
        }
        else
        {
            wantedPosX = rb.transform.position.x;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        waitTimer.Update();

        if (waitTimer.finish && movedDist < maxMovement)
        {
            movedDist += Mathf.Abs(speed) * Time.deltaTime;

            if (vertical)
            {
                wantedPosY += speed * Time.deltaTime;
                rb.transform.position = new Vector3(rb.transform.position.x, wantedPosY, rb.transform.position.z);
            }
            else
            {
                wantedPosX += speed * Time.deltaTime;
                rb.transform.position = new Vector3(wantedPosX, rb.transform.position.y, rb.transform.position.z);
            }
        }
        if (movedDist >= maxMovement)
        {
            speed *= -1;
            waitTimer.Reset();
            waitTimer.start = true;
            movedDist = 0;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if colliding with player, make player move with platform
        if(collision.gameObject.layer == 7)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //on exit, stop moving with platform

        if (collision.gameObject.layer == 7)
        {
            collision.transform.parent = null;
        }
    }
}
