using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapClass : MonoBehaviour
{
    [SerializeField] Material material;
    // Start is called before the first frame update

    public enum Classes
    {
        Angel = 0,
        Devil = 1
    }

    public int playerClass;


    // Update is called once per frame
    void Update()
    {
        Controls();
        SwapColor();
    }

    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerClass != (int)Classes.Devil)
        {
            playerClass = (int)Classes.Devil;
        }
        else if (Input.GetKeyDown(KeyCode.W) && playerClass != (int)Classes.Angel)
        {
            playerClass = (int)Classes.Angel;
        }
    }

    void SwapColor()
    {
        if (playerClass == (int)Classes.Angel)
        {
            material.color = Color.white;
        }
        else if (playerClass == (int)Classes.Devil)
        {
            material.color = Color.black;
        }
    }
}
