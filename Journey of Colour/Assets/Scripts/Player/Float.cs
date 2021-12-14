using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    bool isGrounded;
    [SerializeField] Rigidbody rb;
    [SerializeField] float maxFloatTime, cooldownTime;
    CustomTimer maxFloatTimer, cooldownTimer;
    bool abilityGo;

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
        this.isGrounded = GetComponent<PlayerMovement>().isGrounded;

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
                    abilityGo = true;
                }
            }
            else
            {
                abilityGo = false;
            }


            if (abilityGo)
            {
                GetComponent<PlayerAnimations>().isFloating = true;
                swapClass.swappable = false;
                sinY += SinYIncrement * Time.deltaTime;
                var sinMovement = Mathf.Sin(sinY) * Amplitude;

                rb.position = new Vector2(rb.position.x, rb.position.y + sinMovement);
                rb.constraints = freezeXConstraint;
            }

            if (!abilityGo || maxFloatTimer.finish)
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
