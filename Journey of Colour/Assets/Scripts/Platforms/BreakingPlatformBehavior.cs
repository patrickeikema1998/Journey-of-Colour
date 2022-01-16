using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatformBehavior : MonoBehaviour
{
    float shakeSpeed = 80f;
    float shakeAmount = .03f;

    CustomTimer vibratingTimer, breakingTimer, OnExitingCollisionTimer;
    [SerializeField] float secondsUntilVibrating, secondsUntilBreaking;
    [SerializeField] AudioSource crackSound, breakSound;



    private void Start()
    {
        vibratingTimer = new CustomTimer(secondsUntilVibrating);
        breakingTimer = new CustomTimer(secondsUntilBreaking);
        OnExitingCollisionTimer = new CustomTimer(1);
    }
    private void Update()
    {
        //just updates the timers.
        vibratingTimer.Update();
        breakingTimer.Update();
        OnExitingCollisionTimer.Update();

        //this timer starts on collision
        //when timer is finished, starts to vibrate and starts the breaking timer.
        if (vibratingTimer.finish)
        {
            if(!crackSound.isPlaying) crackSound.Play();
            Vibrate();
            breakingTimer.start = true;
        }

        //this timer starts when the platform starts vibrating
        //when the breaking timer is finished
        //tells the platform spawner to start spawning and destroys the object.
        if (breakingTimer.finish)
        {
            breakSound.Play();
            crackSound.Stop();
            GetComponentInParent<BreakingPlatformSpawner>().respawnTimer.start = true;
            Destroy(gameObject);
        }

        //this timer starts when exiting collision.
        //when the timer is finished, resets all timers.
        if (OnExitingCollisionTimer.finish)
        {
            crackSound.Stop(); ;
            vibratingTimer.Reset();
            vibratingTimer.start = false;
            breakingTimer.Reset();
            breakingTimer.start = false;
            OnExitingCollisionTimer.Reset();
            OnExitingCollisionTimer.start = false;
        }
    }

    void Vibrate()
    {
        //starts vibrating based on sinus waves.
        float posChange = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.position = new Vector3(transform.position.x + posChange, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //starts vibrating timer when colliding with player
        //also resets the exit collision timer.
        if (collision.gameObject.tag == "Player")
        {

            vibratingTimer.start = true;
            OnExitingCollisionTimer.Reset();
            OnExitingCollisionTimer.start = false;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        //when exiting collision, starts the timer.
        if (collision.gameObject.tag == "Player")
        {
            OnExitingCollisionTimer.start = true;
        }
    }



}
