using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapClass : MonoBehaviour
{
    [SerializeField] Material material;
    // Start is called before the first frame update

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
        SwapColor();
    }

    void Controls()
    {
        if (swappable)
        {
            if (Input.GetKeyDown(KeyCode.S))
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

    void SwapColor()
    {
        if (swappable)
        {
            if (currentClass == playerClasses.Angel)
            {
                material.color = Color.white;
            }
            else if (currentClass == playerClasses.Devil)
            {
                material.color = Color.black;
            }
        }
    }
}
