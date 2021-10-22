using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            transform.parent = null;
        }
    }
}
