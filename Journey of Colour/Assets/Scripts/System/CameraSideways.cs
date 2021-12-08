using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSideways : MonoBehaviour
{
    //Camera follows the player from a sideview
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    public GameObject player;
    public Vector3 startOffset;

    private void Start()
    {
        transform.position = player.transform.position - startOffset;
    }

    void LateUpdate()
    {
        Vector3 offset = transform.position - player.transform.position;
        bool changeInX = false;
        bool changeInY = false;
        Vector3 newCameraPosition = transform.position;
        if (offset.x > startOffset.x + 3)
        {
            changeInX = true;
            newCameraPosition.x = (player.transform.position.x + offset.x) - (offset.x - 3);
        }
        else if (offset.x < startOffset.x)
        {
            changeInX = true;
            newCameraPosition.x = (player.transform.position.x);
        }
        if (offset.y > + startOffset.y + 2)
        {
            changeInY = true;
            newCameraPosition.y = (player.transform.position.y + offset.y) - (offset.y - 2);
        }
        else if (offset.y < startOffset.y - 2)
        {
            changeInY = true;
            newCameraPosition.y = (player.transform.position.y + offset.y) - (offset.y + 2);
        }
        if (!changeInX)
        {
            newCameraPosition.x = transform.position.x;
        }
        if (!changeInY)
        {
            newCameraPosition.y = transform.position.y;
        }
        transform.position = newCameraPosition;
        transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z));
    }
}
