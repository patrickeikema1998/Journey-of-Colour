using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelDevilSwitchPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    SwapClass playerClass;
    Collider objCollider;
    Material material;
    CustomTimer swapColorTimer;
    [SerializeField] int swapTimeInSeconds;
    [SerializeField] Color angelColor, devilColor;
    [SerializeField] bool startAsAngel;
    void Start()
    {
        player = GameObject.Find("Player");
        playerClass = player.GetComponent<SwapClass>();
        objCollider = GetComponent<Collider>();
        material = GetComponent<Renderer>().material;
        swapColorTimer = new CustomTimer(swapTimeInSeconds);
        swapColorTimer.start = true;

        //so that you can determine starting color.
        if (startAsAngel) material.color = angelColor;
        else material.color = devilColor;
    }

    // Update is called once per frame
    void Update()
    {
        //updates timer
        swapColorTimer.Update();

        //if timer is finished it switches the colors.
        if (swapColorTimer.finish)
        {
            if(material.color == angelColor)
            {
                material.color = devilColor;

            } else
            {
                material.color = angelColor;
            }
            //resets the timer
            swapColorTimer.Reset();
            swapColorTimer.start = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        //Checks if player is the right class, if it is it sets the trigger on false (player now collides with platform).
        if(other.gameObject == player)
        {
            if (playerClass.IsAngel() && material.color == angelColor || playerClass.IsDevil() && material.color == devilColor)
            {
                objCollider.isTrigger = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //checks if player is the wrong class, if it is sets the trigger on true (player now falls through platform).
        if(collision.gameObject == player)
        {
            if (playerClass.IsDevil() && material.color == angelColor || playerClass.IsAngel() && material.color == devilColor)
            {
                objCollider.isTrigger = true;
            }
        }
    }
}
