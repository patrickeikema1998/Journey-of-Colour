using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public Rigidbody rb;
    public float dashForce = 0;
    public float coolDownTime = 0.2f;

    private float direction;
    private float coolDown;

    void Start()
    {
        coolDown = coolDownTime;
        direction = 1;
    }
    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0) direction = -1;
        if (Input.GetAxis("Horizontal") > 0) direction = 1;

        if (Input.GetKey(KeyCode.Q) && coolDown < 0)
        {
            Dash();
        }
    }

    void Dash()
    {
        coolDown = coolDownTime;
        
        rb.AddForce(new Vector3(direction*dashForce*400,10,0));
    }
}
