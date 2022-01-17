using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] string trigger;
    [SerializeField] float freezeTime;
    [SerializeField] [Range(0f, 10f)] float freezeDistance;

    Vector3 startMovementPos;

    bool triggered;
    GameObject camera, player;
    CustomTimer freezeTimer;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.Find("Player");

        freezeTimer = new CustomTimer(freezeTime);
        //freezeTimer.start = false;
        freezeTimer.finish = true;
    }


    private void Update()
    {
        freezeTimer.Update();
        //Debug.Log(player.transform.position + " startm " + startMovementPos+" timerfinish"+ freezeTimer.finish +" timerem "+ freezeTimer.timeRemaining);

        if (triggered)
        {
            if (player.transform.position.x >= startMovementPos.x || freezeTimer.finish)
            {
                camera.GetComponent<AutomaticScrolling>().moving = true;
                freezeTimer.timeRemaining = 0;
                triggered = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag) {
            case "Player":
                switch (trigger)
                {
                    case "Pause":
                        camera.GetComponent<AutomaticScrolling>().moving = false;
                        Triggered(transform.position, freezeDistance);
                        freezeTimer.Reset();
                        break;
                    default:
                        break;
                }
                break;
        } 
    }

    public void Triggered(Vector3 triggerPos, float distance)
    {
        startMovementPos = new Vector3(
            triggerPos.x + distance,
            triggerPos.y,
            triggerPos.z
            );
        triggered = true;
    }
}
