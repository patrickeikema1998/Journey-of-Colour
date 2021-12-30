using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveWithPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if colliding with player, make player move with platform
        if (collision.gameObject.layer == 7)
        {
            collision.transform.parent = transform;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        //on exit, stop moving with platform

        if (collision.gameObject.layer == 7)
        {
            collision.transform.parent = null;
        }
    }
}
