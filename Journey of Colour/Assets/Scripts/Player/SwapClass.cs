using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapClass : MonoBehaviour
{

    public bool swappable = true;
    public enum playerClasses
    {
        Angel = 0,
        Devil = 1
    }

    public playerClasses currentClass;


    // Update is called once per frame
    void Update()
    {
        Controls();
        SwapPlayer();
    }

    void Controls()
    {
        if (swappable)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(currentClass == playerClasses.Angel)
                {
                    currentClass = playerClasses.Devil;
                } else
                {
                    currentClass = playerClasses.Angel;
                }
            }
        }
    }

    void SwapPlayer()
    {
        if (swappable)
        {

        }
    }
}
