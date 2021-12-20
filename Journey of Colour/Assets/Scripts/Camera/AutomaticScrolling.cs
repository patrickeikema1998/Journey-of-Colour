using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticScrolling : MonoBehaviour
{
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    [SerializeField] Vector3 startPos;
    [SerializeField] Quaternion startRot;
    [SerializeField] float yOffset, normalSpeed, highSpeed;

    float speed;
    float xVelocity;
    float speederOffset = Screen.width * 0.01f;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = startPos;
        transform.rotation = startRot;
    }

    private void Update()
    {
        if (player.transform.position.x >= transform.position.x + speederOffset) { speed = highSpeed; }
        else { speed = normalSpeed; }
    }

    private void LateUpdate()
    {
        float currentYOffset = transform.position.y - player.transform.position.y;
        bool changeInY = false;
        float newCameraPositionY = transform.position.y;

        if (currentYOffset > startPos.y + yOffset)
        {
            changeInY = true;            
            newCameraPositionY = (player.transform.position.y + currentYOffset) - (currentYOffset - yOffset);
        }
        else if (currentYOffset < startPos.y - yOffset )
        {
            changeInY = true;
            newCameraPositionY = (player.transform.position.y + currentYOffset) - (currentYOffset + yOffset);
        }
        if (!changeInY)
        {
            newCameraPositionY = transform.position.y;
        }

        xVelocity = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xVelocity,
            newCameraPositionY,
            transform.position.z);

    }
}
