using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    [SerializeField] float maxFloatTime, cooldownTime;
    CustomTimer maxFloatTimer, cooldownTimer;

    GameObject player;
    Rigidbody rb;
    PlayerAnimations playerAnim;
    PlayerMovement playerMovement;
    SwapClass swapClass;

    [SerializeField] private float floatSpeed, floatDistance;
    [HideInInspector] public bool isFloating;
    bool startedFloating, stoppedFloating;
    [SerializeField] AudioSource sound;

    private void Start()
    {
        maxFloatTimer = new CustomTimer(maxFloatTime);
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;

        player = transform.parent.gameObject;
        rb = player.GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimations>();
        playerMovement = player.GetComponent<PlayerMovement>();
        swapClass = player.GetComponent<SwapClass>();

        stoppedFloating = true;
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

        if (!playerMovement.isGrounded)
        {
            if (Input.GetKeyDown(GameManager.GM.floatAbility) && cooldownTimer.finish)
            {
                isFloating = true;
                stoppedFloating = false;
                startedFloating = false;
            }
            if (Input.GetKeyUp(GameManager.GM.floatAbility))
            {
                isFloating = false;
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
            Debug.Log("should stop floating");
            cooldownTimer.start = true;
            StopFloat();
        }
    }

    void StartFloat()
    {
        if (!startedFloating)
        {
            sound.Play();

            startedFloating = true;
            //timer
            maxFloatTimer.start = true;

            //animation
            playerAnim.Floating(true);

            //stopping movement and constrains.
            rb.useGravity = false;
            swapClass.swappable = false;
            playerMovement.canMove = false;
            playerMovement.canTurn = false;
            rb.velocity = Vector3.zero;
        }
        
        //tiny movement in mid air based on sinus waves.
        var pos = player.transform.position;
        player.transform.position = new Vector3(pos.x, pos.y + (Mathf.Sin(Time.fixedTime * floatSpeed) * floatDistance), pos.z);
    }

    void StopFloat()
    {
        sound.Stop();

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
        playerMovement.canTurn = true;
        rb.useGravity = true;
    }
}
