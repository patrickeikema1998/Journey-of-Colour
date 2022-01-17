using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticScrolling : MonoBehaviour
{
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    [SerializeField] [Range(0f, 5f)] float yOffset;
    [SerializeField] public float normalSpeed, highSpeed/*, freezeTime*/;
    [SerializeField] [Range(0f, 1f)] float speedTriggerPercentage;
    
    [HideInInspector] public bool moving/*, frozen*/;
    [HideInInspector] public bool speedTrigger;
    

    Vector3 /*startMovementPos,*/ offset;
    float speed;
    float xSpeed;
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

        screenSize = GetComponent<Camera>().pixelRect;
        screenSizeCalc = GetComponent<Camera>().WorldToViewportPoint(new Vector3(screenSize.width, screenSize.height, 0f));
        speederOffset = (screenSizeCalc.x / 2) - (screenSizeCalc.x * speedTriggerPercentage);

        speed = normalSpeed;
        moving = true;
    }


    private void LateUpdate()
    {
        float currentYOffset = transform.position.y - player.transform.position.y;
        bool changeInY = false;
        float newCameraPositionY = transform.position.y;

        if (currentYOffset > startYOffset + yOffset)
        {
            changeInY = true;
            newCameraPositionY = (player.transform.position.y + startYOffset + currentYOffset) - (currentYOffset - yOffset);
        }
        else if (currentYOffset < startYOffset - yOffset)
        {
            changeInY = true;
            newCameraPositionY = (player.transform.position.y + startYOffset + currentYOffset) - (currentYOffset + yOffset);
        }
        if (!changeInY)
        {
            newCameraPositionY = transform.position.y;
        }

        if (player.GetComponent<PlayerHealth>().GetHealth <= 0)
        {
            speed = 0;
            moving = false;
        }

        xSpeed = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xSpeed,
            newCameraPositionY,
            transform.position.z);

        if (moving)
        {

            //Debug.Log(speed);
            switch (OverSpeederLimit()) {
                case true:
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
        if (player.transform.position.x >= transform.position.x + speederOffset) { return true; }
        else { return false; }
    }

    public void Reset()
    {
        transform.position = player.transform.position + offset;
        moving = true;
    }
}
