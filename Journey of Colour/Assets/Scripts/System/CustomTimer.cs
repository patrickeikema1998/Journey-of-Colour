using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimer : MonoBehaviour
{
    /*
     This is a simple Timer class. You can instantiate one in any component.
     At start the start bool is false, so the timer wont start counting.
     Dont forget to call the timerName.Update() in the update of your component.
     If you want to test if the timer is finished, check the bool finish.
     To reset the timer, call on Reset().
    */


    public float timeRemaining, initialTimeInSeconds;
    public bool start, finish;

    public CustomTimer(float timeInSeconds)
    {
        initialTimeInSeconds = timeInSeconds;
        timeRemaining = initialTimeInSeconds;
    }


    // Update is called once per frame
    public void Update()
    {
        if (start)
        {
            if (timeRemaining > 0)
            {
                //subtracts realtime seconds from time remaining
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                finish = true;
                start = false;
                timeRemaining = initialTimeInSeconds;
            }
        }
    }

    public void Reset()
    {
        timeRemaining = initialTimeInSeconds;
        finish = false;
        start = true;
    }
}
