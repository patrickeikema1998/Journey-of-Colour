using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSideways : MonoBehaviour
{
    //Camera follows the player from a sideview
    //https://answers.unity.com/questions/299102/improve-smooth-2d-side-scroller-camera-to-look-mor.html
    [SerializeField] float xDistance, yDistance;
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
        if (offset.x > startOffset.x - xDistance && player.GetComponent<PlayerMovement>().lookingLeft)
        {
            //player to left
            changeInX = true;
            newCameraPosition.x = (player.transform.position.x + offset.x) - (offset.x + xDistance);
        }
        else if (offset.x < startOffset.x + xDistance && !player.GetComponent<PlayerMovement>().lookingLeft)
        {
            changeInX = true;
            newCameraPosition.x = (player.transform.position.x + offset.x) - (offset.x - xDistance);
        }
        if (offset.y > + startOffset.y + yDistance)
        {
            changeInY = true;
            newCameraPosition.y = (player.transform.position.y + offset.y) - (offset.y - yDistance);
        }
        else if (offset.y < startOffset.y - yDistance)
        {
            changeInY = true;
            newCameraPosition.y = (player.transform.position.y + offset.y) - (offset.y + yDistance);
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
