using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatformBehavior : MonoBehaviour
{
    float speed = 80f; //how fast it shakes
    float amount = .03f; //how much it shakes

    CustomTimer vibratingTimer, breakingTimer, resetOnCollisionExitTimer;
    [SerializeField] float secondsUntilVibrating, secondsUntilBreaking;

    private void Start()
    {
        resetOnCollisionExitTimer = new CustomTimer(2);
        vibratingTimer = new CustomTimer(secondsUntilVibrating);
        breakingTimer = new CustomTimer(secondsUntilBreaking);

    }
    private void Update()
    {
        resetOnCollisionExitTimer.Update();
        vibratingTimer.Update();
        breakingTimer.Update();

        if (vibratingTimer.finish)
        {
            Vibrate();
            breakingTimer.start = true;
        }

        if (breakingTimer.finish)
        {
            GetComponentInParent<BreakingPlatformSpawner>().go = true;
            Destroy(gameObject);
        }

        if (resetOnCollisionExitTimer.finish)
        {
            vibratingTimer.Reset();
            vibratingTimer.start = false;
            breakingTimer.Reset();
            breakingTimer.start = false;
            resetOnCollisionExitTimer.Reset();
            resetOnCollisionExitTimer.start = false;
        }
    }

    void Vibrate()
    {
        float posChange = Mathf.Sin(Time.time * speed) * amount;
        transform.position = new Vector3(transform.position.x + posChange, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            vibratingTimer.start = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            resetOnCollisionExitTimer.start = true;
        }
    }



}
