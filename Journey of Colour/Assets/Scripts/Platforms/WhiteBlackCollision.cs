using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlackCollision : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;
    SwapClass playerClass;
    Collider collider;
    CustomTimer swapColorTimer;
    [SerializeField] Material material;
    [SerializeField] int swapTimeInSeconds;
    [SerializeField] bool startAsBlack;

    string color;
    void Start()
    {
        swapColorTimer = new CustomTimer(swapTimeInSeconds);
        swapColorTimer.start = true;

        playerClass = player.GetComponent<SwapClass>();
        collider = GetComponent<Collider>();

        if (startAsBlack) material.color = Color.black;
        else material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        swapColorTimer.Update();

        if (swapColorTimer.finish)
        {
            if(material.color == Color.white)
            {
                material.color = Color.black;

            } else
            {
                material.color = Color.white;
            }
            swapColorTimer.Reset();
            swapColorTimer.start = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            // 0 is white
            if (playerClass.currentClass == SwapClass.playerClasses.Angel && material.color == Color.white)
            {
                collider.isTrigger = false;
            }
            
            if(playerClass.currentClass == SwapClass.playerClasses.Devil && material.color == Color.black)
            {
                collider.isTrigger = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if(playerClass.currentClass == SwapClass.playerClasses.Devil && material.color == Color.white)
            {
                collider.isTrigger = true;
            }
            if(playerClass.currentClass == SwapClass.playerClasses.Angel && material.color == Color.black)
            {
                collider.isTrigger = true;
            }

            

        }
    }
}
