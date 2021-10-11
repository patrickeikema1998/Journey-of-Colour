using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    // Start is called before the first frame update

    bool isGrounded;
    [SerializeField] Rigidbody rb;
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.isGrounded = GetComponent<PlayerMovement>().isGrounded;

        Vector3 v = Physics.gravity * rb.mass;

        if ( !isGrounded )
        {
            if (Input.GetKey(KeyCode.E))
            {
                //rb.AddForce(-v);
                rb.useGravity = false;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            } else
            {
                rb.useGravity = true;
            }
        }
    }

}
