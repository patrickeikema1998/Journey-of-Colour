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
    bool abilityGo;

    SwapClass swapClass;

    private float sinY;
    [SerializeField] private float SinYIncrement;
    [SerializeField] private float Amplitude;

    private void Start()
    {
        maxFloatTimer = new CustomTimer(maxFloatTime);
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;

        swapClass = GetComponent<SwapClass>();

    }

    private void Update()
    {

        maxFloatTimer.Update();
        cooldownTimer.Update();
    }

    private void FixedUpdate()
    {
        this.isGrounded = GetComponent<PlayerMovement>().isGrounded;

        if (!isGrounded && swapClass.playerClass == 0)
        {
            if (Input.GetKey("s"))
            {
                if (cooldownTimer.finish)
                {
                    cooldownTimer.Reset();
                    cooldownTimer.start = true;

                    maxFloatTimer.start = true;
                    rb.useGravity = false;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    abilityGo = true;
                }
            } else
            {
                abilityGo = false;
            }


            if (abilityGo)
            {
                sinY += SinYIncrement * Time.deltaTime;
                var sinMovement = Mathf.Sin(sinY) * Amplitude;

                rb.position = new Vector2(rb.position.x, rb.position.y + sinMovement);
            }

            if (!abilityGo || maxFloatTimer.finish)
            {
                rb.useGravity = true;
                maxFloatTimer.Reset();
            }
        }
    }

}
