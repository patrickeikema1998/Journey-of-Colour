using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float dashForce = 80;
    [SerializeField] float coolDownTime = 0.5f;
    [SerializeField] float durationTime = 0.4f;

    private float direction;
    private float coolDown;
    private float duration;
    SwapClass swapClass;

    void Start()
    {
        coolDown = coolDownTime;
        swapClass = GetComponent<SwapClass>();
        duration = 0;
        direction = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
        coolDown -= Time.deltaTime;
        duration -= Time.deltaTime;

        //checks the last direction the player is facing
        if (Input.GetAxis("Horizontal") < 0) direction = -1;
        if (Input.GetAxis("Horizontal") > 0) direction = 1;

        //Input check for dash ability
        if (Input.GetMouseButtonDown(0) && coolDown < 0 && swapClass.IsAngel())
        {
            duration = durationTime;
        }

        if (duration > 0)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            Dash();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        //makes sure that the player's z-axis reamians zero
        if (rb.transform.position.z != 0)
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 0);
    }

    
    void Dash()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(direction*dashForce,0,0));
        coolDown = coolDownTime;
        rb.velocity = Vector3.zero;
    }
}
