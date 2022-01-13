using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAndYMovingPlatform : MonoBehaviour
{
    CustomTimer waitTimer;
    [SerializeField] float maxMovement, waitTime;
    [SerializeField] [Tooltip("Make speed negative if the platform should start going left or downwards.")] float speed;
    [SerializeField] bool vertical, horizontal;

    float movedDist;
    float xPos, yPos;


    // Start is called before the first frame update
    void Start()
    {
        waitTimer = new CustomTimer(waitTime);
        waitTimer.start = true;

        yPos = transform.position.y;
        xPos = transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        waitTimer.Update();

        if (waitTimer.finish && movedDist < maxMovement)
        {
            // this simply keeps track of moved distance
            movedDist += Mathf.Abs(speed) * Time.deltaTime;

            //moves the platform vertical or horizontal
            if (vertical)
            {
                yPos += speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            }
            if(horizontal)
            {
                xPos += speed * Time.deltaTime;
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }
        }

        //if max movement is reached: reverse speed, reset timer and movedDist
        if (movedDist >= maxMovement)
        {
            speed *= -1;
            waitTimer.Reset();
            waitTimer.start = true;
            movedDist = 0;
        }
    }
}
 