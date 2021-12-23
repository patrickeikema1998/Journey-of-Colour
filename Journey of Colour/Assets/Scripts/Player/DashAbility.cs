using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float dashForce = 10;
    [SerializeField] float coolDownTime = 0.2f;
    [SerializeField] float durationTime = 0.25f;

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
    }

    
    void Dash()
    {
        rb.AddForce(new Vector3(direction*dashForce,0,0));
        coolDown = coolDownTime;
    }
}
