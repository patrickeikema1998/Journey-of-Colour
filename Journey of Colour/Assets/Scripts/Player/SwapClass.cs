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

    private void Start()
    {
        currentClass = playerClasses.Angel;
    }

    // Update is called once per frame
    void Update()
    {
        SwapPlayerClass();
    }

    void SwapPlayerClass()
    {
        if (swappable)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(currentClass == playerClasses.Angel)
                {
                    currentClass = playerClasses.Devil;
                    devil.SetActive(true);
                    angel.SetActive(false);
                } else
                {
                    currentClass = playerClasses.Angel;
                    devil.SetActive(false);
                    angel.SetActive(true);
                }
            }
        }
    }

    public bool IsAngel()
    {
        if (currentClass == playerClasses.Angel) return true;
        else return false;
    }

    public bool IsDevil()
    {
        if (currentClass == playerClasses.Devil) return true;
        else return false;
    }
}
