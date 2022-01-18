using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticScrolling : MonoBehaviour
{
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    [SerializeField] public Vector2 yOffset;
    [SerializeField] public float normalSpeed, highSpeed/*, freezeTime*/;
    [SerializeField] [Range(0f, 1f)] float speedTriggerPercentage;
    
    [HideInInspector] public bool moving/*, frozen*/;
    [HideInInspector] public bool speedTrigger;
    

    Vector3 /*startMovementPos,*/ offset;
    float speed;
    [HideInInspector] public float xSpeed;
    float speederOffset;
    float startYOffset;

    Rect screenSize;
    Vector2 screenSizeCalc;
    GameObject player;
    //CustomTimer freezeTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
        startYOffset = transform.position.y - player.transform.position.y;

        //Calculate the distance to the left and right side of the screen in worldspace
        screenSize = GetComponent<Camera>().pixelRect;
        screenSizeCalc = GetComponent<Camera>().WorldToViewportPoint(new Vector3(screenSize.width, screenSize.height, 0f));
        //Calculate the distance used to check if the player has reached the right side of the screen
        speederOffset = (screenSizeCalc.x / 2) - (screenSizeCalc.x * speedTriggerPercentage);

        speed = normalSpeed;
        moving = true;
    }


    private void LateUpdate()
    {
        float currentYOffset = transform.position.y - player.transform.position.y;
        bool changeInY = false;
        float newCameraPositionY = transform.position.y;

        //Checks if the player Moved along the y-axis
        //and moves if a certain distance along the y-axis had been met
        if (currentYOffset > startYOffset + yOffset.x)
        {
            //Up
            changeInY = true;
            newCameraPositionY = (player.transform.position.y + startYOffset + currentYOffset) - (currentYOffset - yOffset.x);
        }
        else if (currentYOffset < startYOffset - yOffset.y)
        {
            //Down
            changeInY = true;
            newCameraPositionY = (player.transform.position.y + startYOffset + currentYOffset) - (currentYOffset + yOffset.y);
        }
        if (!changeInY)
        {
            //No movement (movement not big enough)
            newCameraPositionY = transform.position.y;
        }

        if (player.GetComponent<PlayerHealth>().GetHealth <= 0)
        {
            //Player is dead so the camera stops moving
            speed = 0;
            moving = false;
        }

        //Calculates the movement of the camera (no velocity since a constant speed was desired)
        xSpeed = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xSpeed,
            newCameraPositionY,
            transform.position.z);

        if (moving)
        {
            //if the camera moves and the right side of the screen has been reached
            switch (OverSpeederLimit()) {
                case true:
                    //right side of the screen
                    speed = highSpeed;
                    break;
                case false:
                    speed = normalSpeed;
                    break;
            } 
        }
        else 
        {
            speed = 0;
        }
    }

    bool OverSpeederLimit() 
    {
        //If right side of the screen return true
        if (player.transform.position.x >= transform.position.x + speederOffset) { return true; }
        else { return false; }
    }

    public void Reset()
    {
        //Resets the camera offset to the starting camera offset
        transform.position = player.transform.position + offset;
        moving = true;
    }
}
