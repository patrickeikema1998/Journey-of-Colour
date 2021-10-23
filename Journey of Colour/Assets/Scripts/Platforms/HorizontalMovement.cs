using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float maxMovement, speed, waitTime;
    float movedDist;
    CustomTimer waitTimer;

    float wantedPosX;


    // Start is called before the first frame update
    void Start()
    {
        waitTimer = new CustomTimer(waitTime);
        waitTimer.start = true;

        wantedPosX = rb.transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        waitTimer.Update();

        if (waitTimer.finish && movedDist < maxMovement)
        {
            movedDist += Mathf.Abs(speed) * Time.deltaTime;
            //rb.velocity = new Vector3(speed, 0, 0);
            wantedPosX += speed * Time.deltaTime;
            rb.transform.position = new Vector3(wantedPosX, rb.transform.position.y, rb.transform.position.z);


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
}
