using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapClass : MonoBehaviour
{
    [SerializeField]GameObject angel, devil;
    public bool swappable = true;
    PlayerHealth playerHealth;
    public enum playerClasses
    {
        Angel = 0,
        Devil = 1
    }

    public playerClasses currentClass;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        currentClass = playerClasses.Angel;
    }

    // Update is called once per frame
    void Update()
    {
        SwapPlayerClass();
    }


    //this method swaps the playerclass enum and enables the corresponding gameobject.
    void SwapPlayerClass()
    {
        if (swappable && !playerHealth.dead)
        {
            if (Input.GetKeyDown(GameManager.GM.switchPlayer))
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

    //checks if the current class is angel or devil
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
