using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimer : MonoBehaviour
{
    public float timeRemaining, initialTimeInSeconds;
    public bool start, finish; 
    // Start is called before the first frame update

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
        start = false;
        finish = false;
    }
}
