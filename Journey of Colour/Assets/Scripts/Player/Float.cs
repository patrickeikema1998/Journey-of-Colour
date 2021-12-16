using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float maxFloatTime, cooldownTime;
    CustomTimer maxFloatTimer, cooldownTimer;
    public bool isFloating;

    SwapClass swapClass;

    private float sinY;
    [SerializeField] private float SinYIncrement;
    [SerializeField] private float Amplitude;

    RigidbodyConstraints freezeXConstraint, normalConstraints;

    private void Start()
    {
        maxFloatTimer = new CustomTimer(maxFloatTime);
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;

        swapClass = GetComponent<SwapClass>();

        normalConstraints = rb.constraints;
        freezeXConstraint = normalConstraints | RigidbodyConstraints.FreezePositionX;
    }

    private void Update()
    {

        maxFloatTimer.Update();
        cooldownTimer.Update();
    }

    private void FixedUpdate()
    {
        bool isGrounded = GetComponent<PlayerMovement>().canJump;


        if (!isGrounded && swapClass.currentClass == 0)
        {
            if (Input.GetKey("s"))
            {
                if (cooldownTimer.finish)
                {
                    cooldownTimer.Reset();
                    cooldownTimer.start = true;

                    maxFloatTimer.start = true;
                    rb.useGravity = false;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);         //sets Y velocity on 0 before starting ability, so that player wont fly upwards.
                    isFloating = true;
                }
            }
            else
            {
                isFloating = false;
            }


            if (isFloating)
            {
                GetComponent<PlayerAnimations>().isFloating = true;
                swapClass.swappable = false;
                sinY += SinYIncrement * Time.deltaTime;
                var sinMovement = Mathf.Sin(sinY) * Amplitude;

                rb.position = new Vector2(rb.position.x, rb.position.y + sinMovement);
                rb.constraints = freezeXConstraint;
            }

            if (!isFloating || maxFloatTimer.finish)
            {
                GetComponent<PlayerAnimations>().isFloating = false;
                rb.constraints = normalConstraints;
                rb.useGravity = true;
                maxFloatTimer.Reset();
                swapClass.swappable = true;
            }
        }
    }

}
