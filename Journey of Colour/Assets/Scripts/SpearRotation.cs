using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRotation : MonoBehaviour
{
    Rigidbody rb;
    float divider = 15f;
    float angle;
    float startRotationZ, downRotationZ;
    Vector3 force;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angle = 0f;
        rb.AddForce(new Vector3(-2000, 0, 0));
        startRotationZ = rb.rotation.eulerAngles.z;
        downRotationZ = 180f;
    }


    void Update()
    {
        if(rb.velocity.y < 0)
        {
            angle = Mathf.Lerp(startRotationZ, downRotationZ, -rb.velocity.y / divider);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionStay(Collision collision)
    { 
    }
}
