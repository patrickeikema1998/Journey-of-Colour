using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticScrolling : MonoBehaviour
{
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    [SerializeField] [Range(0f, 5f)] float yOffset;
    [SerializeField] float normalSpeed, highSpeed, freezeTime;
    [SerializeField] [Range(0f, 1f)] float speedTriggerPercentage;
    [SerializeField] [Range(0f, 10f)] float triggerDistance;
    
    [HideInInspector] public bool moving;
    bool triggered;

    Vector3 startMovementPos, offset;
    float speed;
    float xVelocity;
    float speederOffset;
    float startYOffset;

    Rect screenSize;
    Vector2 screenSizeCalc;
    GameObject player;
    CustomTimer freezeTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
        startYOffset = transform.position.y - player.transform.position.y;

        screenSize = GetComponent<Camera>().pixelRect;
        screenSizeCalc = GetComponent<Camera>().WorldToViewportPoint(new Vector3(screenSize.width, screenSize.height, 0f));
        speederOffset = (screenSizeCalc.x / 2) - (screenSizeCalc.x * speedTriggerPercentage);
       
        moving = true;

        freezeTimer = new CustomTimer(freezeTime);
        freezeTimer.start = false;
        freezeTimer.finish = true;
    }

    private void Update()
    {
        freezeTimer.Update();

        if (triggered) 
        {
            if (player.transform.position.x >= startMovementPos.x) 
            { 
                moving = true;
                freezeTimer.timeRemaining = 0;
                triggered = false;
            }
        }
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
        else if (currentYOffset < startYOffset - yOffset )
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

        xVelocity = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xVelocity,
            newCameraPositionY,
            transform.position.z);
        
        if (moving && freezeTimer.finish)
        {
            Debug.Log("player: "+ player.transform.position.x + " | trigger: " + (transform.position.x + speederOffset) + " | speed: " + speed);
            if (player.transform.position.x >= transform.position.x + speederOffset) { speed = highSpeed; }
            else { speed = normalSpeed; }
        }
        else 
        {
            if (!moving)
            {
                freezeTimer.Reset();
                freezeTimer.start = true;
                freezeTimer.finish = false;
            }

            speed = 0;
            moving = true;
        }
    }

    public void Triggered(Vector3 triggerPos) 
    {
        startMovementPos = new Vector3(
            triggerPos.x + triggerDistance,
            triggerPos.y,
            triggerPos.z
            );
        triggered = true;
    }

    public void Reset()
    {
        transform.position = player.transform.position + offset;
        moving = true;
    }
}
