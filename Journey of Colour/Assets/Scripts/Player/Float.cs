using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    [SerializeField] float maxFloatTime, cooldownTime;
    CustomTimer maxFloatTimer, cooldownTimer;

    Rigidbody rb;
    PlayerAnimations playerAnim;
    PlayerMovement playerMovement;
    SwapClass swapClass;

    [SerializeField] private float floatSpeed, floatDistance;
    [HideInInspector] public bool isFloating;
    bool stoppedFloating;

    private void Start()
    {
        maxFloatTimer = new CustomTimer(maxFloatTime);
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;

        rb = GetComponent<Rigidbody>();
        playerAnim = GameObject.Find("Angel Player").GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        swapClass = GetComponent<SwapClass>();
    }

    private void Update()
    {
        maxFloatTimer.Update();
        cooldownTimer.Update();

        CheckAndHandleInput();
    }

    private void FixedUpdate()
    {
        HandleFloatAbility();
    }

    void CheckAndHandleInput()
    {
        bool isGrounded = GetComponent<PlayerMovement>().isGrounded;

        if (!isGrounded && swapClass.IsAngel())
        {
            if (Input.GetKeyDown(KeyCode.S) && cooldownTimer.finish)
            {
                isFloating = true;
                stoppedFloating = false;
                
            }
            if(Input.GetKeyUp(KeyCode.S))
            {
                isFloating = false;
                stoppedFloating = true;
            }
        }

    }

    void HandleFloatAbility()
    {
        if (isFloating && !maxFloatTimer.finish)
        {
            cooldownTimer.Reset();
            cooldownTimer.start = false;
            StartFloat();
        }
        if ((!isFloating || maxFloatTimer.finish) && !stoppedFloating)
        {
            cooldownTimer.start = true;
            StopFloat();
        }
    }

    void StartFloat()
    {
        //timer
        maxFloatTimer.start = true;

        //animation
        playerAnim.Floating(true);

        //stopping movement and constrains.
        rb.useGravity = false;
        swapClass.swappable = false;
        playerMovement.canMove = false;
        rb.velocity = Vector3.zero;

        //tiny movement in mid air based on sinus waves.
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + (Mathf.Sin(Time.fixedTime * floatSpeed) * floatDistance), pos.z);
    }

    void StopFloat()
    {
        isFloating = false;
        stoppedFloating = true;
        //timers
        maxFloatTimer.Reset();
        maxFloatTimer.start = false;

        //animation
        playerAnim.Floating(false);

        //constrains
        swapClass.swappable = true;
        playerMovement.canMove = true;
        rb.useGravity = true;
    }
}
