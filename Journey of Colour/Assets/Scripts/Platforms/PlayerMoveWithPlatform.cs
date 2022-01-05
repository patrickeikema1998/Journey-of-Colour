using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveWithPlatform : MonoBehaviour
{
    //simple script that sets the transform of the player as a child of the transform of the object.
    //This ensures that the player moves with the object.
    private void OnCollisionEnter(Collision collision)
    {
        //if colliding with player, make player move with platform
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        //on exiting collision, stop moving with platfo
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
