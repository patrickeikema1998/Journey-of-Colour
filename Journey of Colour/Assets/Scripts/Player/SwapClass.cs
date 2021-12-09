using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapClass : MonoBehaviour
{
    [SerializeField]GameObject angel, devil;
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
        if (currentClass == playerClasses.Angel)
        {
            devil.SetActive(false);
            angel.SetActive(true);
        } else
        {
            devil.SetActive(true);
            angel.SetActive(false);
        }
    }
}
