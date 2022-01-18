using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] string trigger;
    [SerializeField] float freezeTime, speedChange, lerpSpeed, lerpDistance;
    [SerializeField] [Range(0f, 10f)] float freezeDistance;

    [SerializeField] Vector3 WantedMove;
    Vector3 startMovementPos, targetPos;

    bool triggered;
    GameObject camera, player;
    CustomTimer freezeTimer;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.Find("Player");

        switch (trigger) 
        {
            //sets the needed variables per desired action
            case "Pause":
                PauseStart();
                break;
            default:
                break;
        }
    }


    private void Update()
    {
        //Updates the desired action
        if (triggered)
        {
            switch (trigger)
            {
                case "Pause":
                    PauseUpdate();
                    break;
                case "Speed":
                    SpeedUpdate();
                    break;
                case "Lerp":
                    LerpUpdate();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the player triggered the gameObject and which action is desired
        switch (other.tag) {
            case "Player":
                switch (trigger)
                {
                    case "Pause":
                        camera.GetComponent<AutomaticScrolling>().moving = false;
                        Triggered(transform.position, freezeDistance);
                        freezeTimer.Reset();
                        triggered = true;
                        break;
                    case "Lerp":
                        targetPos = new Vector3(
                            camera.transform.position.x + WantedMove.x,
                            camera.transform.position.y + WantedMove.y,
                            camera.transform.position.z + WantedMove.z
                            );
                        triggered = true;
                        break;
                    default:
                        triggered = true;
                        break;
                }
                break;
            default:
                break;
        }
    }

    //Sets timers
    void PauseStart()
    {
        freezeTimer = new CustomTimer(freezeTime);
        freezeTimer.finish = true;
    }

    //Updates the timer and checks if the player has reached the position
    //at which the movement will be restarted
    void PauseUpdate()
    {
        freezeTimer.Update();

        if (player.transform.position.x >= startMovementPos.x || freezeTimer.finish)
        {
            camera.GetComponent<AutomaticScrolling>().moving = true;
            freezeTimer.timeRemaining = 0;
            triggered = false;
            Invoke("InactivateObject", 0f);
        }
    }

    void SpeedUpdate() 
    {
        //Sets the speeds to the new values
        camera.GetComponent<AutomaticScrolling>().speedTrigger = true;
        camera.GetComponent<AutomaticScrolling>().normalSpeed = camera.GetComponent<AutomaticScrolling>().normalSpeed + speedChange;
        camera.GetComponent<AutomaticScrolling>().highSpeed = camera.GetComponent<AutomaticScrolling>().highSpeed + speedChange;

        triggered = false;
        Invoke("InactivateObject", 0f);
    }
    void LerpUpdate()
    {
        //Lerps to the newly desired position (targetPos)
        camera.transform.position = new Vector3(
            Mathf.Lerp(camera.transform.position.x, targetPos.x, lerpSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.y, targetPos.y, lerpSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.z, targetPos.z, lerpSpeed * Time.deltaTime));

        //Destroys object when desired distance has been reached

        targetPos = new Vector3(
            targetPos.x + camera.GetComponent<AutomaticScrolling>().xSpeed,
            targetPos.y, 
            targetPos.z);

        //Debug.Log(Mathf.Abs(Vector3.Distance(camera.transform.position, targetPos));
        if (Mathf.Abs(Vector3.Distance(camera.transform.position, targetPos)) <= lerpDistance)
        {
            triggered = false;
            Invoke("InactivateObject", 0f);
        }
    }

    //Pause state has been triggered, saves the position which will activate movement
    public void Triggered(Vector3 triggerPos, float distance)
    {
        startMovementPos = new Vector3(
            triggerPos.x + distance,
            triggerPos.y,
            triggerPos.z
            );
    }

    //Disables the object
    void InactivateObject()
    {
        this.enabled = false;
    }
}
