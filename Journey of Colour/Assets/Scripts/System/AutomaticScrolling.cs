using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticScrolling : MonoBehaviour
{
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    [SerializeField] Vector3 startPos;
    [SerializeField] float yOffset, normalSpeed, highSpeed;

    float speed;
    float xVelocity;
    float speederOffset = Screen.width * 0.0075f;
    float startYOffset;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = startPos;
        startYOffset = transform.position.y - player.transform.position.y;
    }

    private void LateUpdate()
    {
        float currentYOffset = transform.position.y - player.transform.position.y;
        bool changeInY = false;
        float newCameraPositionY = transform.position.y;

        Debug.Log(currentYOffset+" plus: "+ (startYOffset + yOffset)+" minus: "+ (startYOffset - yOffset));
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

        xVelocity = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xVelocity,
            newCameraPositionY,
            transform.position.z);

        if (player.transform.position.x >= transform.position.x + speederOffset) { speed = highSpeed; }
        else { speed = normalSpeed; }
    }
}
