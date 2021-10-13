using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float maxMovement, speed, waitTime;
    float movedDist;
    CustomTimer waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        waitTimer = new CustomTimer(waitTime);
        waitTimer.start = true;
    }

    // Update is called once per frame
    private void Update()
    {
        waitTimer.Update();

        if (waitTimer.finish && movedDist < maxMovement)
        {
            movedDist += Mathf.Abs(speed) * Time.deltaTime;
            rb.velocity = new Vector3(speed, 0, 0);

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
