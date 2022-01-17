using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] string trigger;
    [SerializeField] float freezeTime, speedChange;
    [SerializeField] [Range(0f, 10f)] float freezeDistance;

    Vector3 startMovementPos;

    bool triggered;
    GameObject camera, player;
    CustomTimer freezeTimer;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.Find("Player");

        switch (trigger) {

            case "Pause":
                PauseStart();
                break;
            default:
                break;
        }
    }


    private void Update()
    {
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
                default:
                    break;
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

    void PauseStart()
    {
        freezeTimer = new CustomTimer(freezeTime);
        freezeTimer.finish = true;
    }

    void PauseUpdate()
    {
        freezeTimer.Update();

        if (player.transform.position.x >= startMovementPos.x || freezeTimer.finish)
        {
            camera.GetComponent<AutomaticScrolling>().moving = true;
            freezeTimer.timeRemaining = 0;
            triggered = false;
            Invoke("DestroyObject", 0f);
        }
    }

    void SpeedUpdate() 
    {
        camera.GetComponent<AutomaticScrolling>().speedTrigger = true;
        camera.GetComponent<AutomaticScrolling>().normalSpeed = camera.GetComponent<AutomaticScrolling>().normalSpeed + speedChange;
        camera.GetComponent<AutomaticScrolling>().highSpeed = camera.GetComponent<AutomaticScrolling>().highSpeed + speedChange;
        Debug.Log(camera.GetComponent<AutomaticScrolling>().normalSpeed + " save me " + camera.GetComponent<AutomaticScrolling>().highSpeed);
        triggered = false;
            Invoke("DestroyObject", 0f);
    }

    public void Triggered(Vector3 triggerPos, float distance)
    {
        startMovementPos = new Vector3(
            triggerPos.x + distance,
            triggerPos.y,
            triggerPos.z
            );
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
