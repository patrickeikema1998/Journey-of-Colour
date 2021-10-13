using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float dashForce = 10;
    [SerializeField] float coolDownTime = 0.2f;
    [SerializeField] float durationTime = 0.25f;
    [SerializeField] Material material;

    private float direction;
    private float coolDown;
    private float duration;

    void Start()
    {
        coolDown = coolDownTime;
        duration = 0;
        direction = 1;
    }
    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        duration -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0) direction = -1;
        if (Input.GetAxis("Horizontal") > 0) direction = 1;

        if (Input.GetKey(KeyCode.W) && coolDown < 0 && this.material.color == Color.white)
        {
            duration = durationTime;
        }

        if (duration > 0) Dash();
    }

    void Dash()
    {
        
        rb.AddRelativeForce(new Vector3(direction*dashForce*100,0,0));
        coolDown = coolDownTime;
    }
}
