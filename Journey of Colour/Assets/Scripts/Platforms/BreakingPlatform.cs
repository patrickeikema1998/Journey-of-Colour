using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    float speed = 80f; //how fast it shakes
    float amount = .08f; //how much it shakes

    CustomTimer vibratingTimer, breakingTimer, returningTimer;
    [SerializeField] float secondsUntilVibrating, secondsUntilBreaking, secondsUntilReturnPlatform;

    private void Start()
    {
        returningTimer = new CustomTimer(secondsUntilReturnPlatform);
        vibratingTimer = new CustomTimer(secondsUntilVibrating);
        breakingTimer = new CustomTimer(secondsUntilBreaking);

    }
    private void Update()
    {
        vibratingTimer.Update();
        breakingTimer.Update();
        returningTimer.Update();

        if (returningTimer.finish)
        {

            returningTimer.Reset();
            returningTimer.start = false;
            GetComponent<BoxCollider>().isTrigger = false;

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
            breakingTimer.start = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(vibratingTimer.timeRemaining);

            if (vibratingTimer.finish)
            {
                Vibrate();
                Debug.Log("vibrating timer finished");
            }
            
            if (breakingTimer.finish)
            {
                Debug.Log("now triggers!");
                collision.transform.parent = null;
                returningTimer.start = true;
                GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collision exit");

        vibratingTimer.Reset();
        vibratingTimer.start = false;
    }

}
