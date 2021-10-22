using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    // Start is called before the first frame update

    bool isGrounded;
    [SerializeField] Rigidbody rb;
    [SerializeField] float maxFloatTime, cooldownTime;
    CustomTimer maxFloatTimer, cooldownTimer;
    bool pressed;

    private void Start()
    {
        maxFloatTimer = new CustomTimer(maxFloatTime);
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;
    }

    private void Update()
    {
        maxFloatTimer.Update();
        cooldownTimer.Update();
    }

    private void FixedUpdate()
    {
        this.isGrounded = GetComponent<PlayerMovement>().isGrounded;

        if (!isGrounded)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (cooldownTimer.finish)
                {
                    cooldownTimer.Reset();
                    cooldownTimer.start = true;

                    maxFloatTimer.start = true;
                    rb.useGravity = false;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                pressed = true;
            } else
            {
                pressed = false;
            }

            if (maxFloatTimer.finish || !pressed)
            {
                rb.useGravity = true;
                maxFloatTimer.Reset();
            }
        }
    }

}
