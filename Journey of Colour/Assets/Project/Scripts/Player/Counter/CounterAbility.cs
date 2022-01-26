using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAbility : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    [Range(0, 4)] public float playerHeight;
    public GameObject CounterPrefab;
    private bool keyDown, once;
    private SwapClass swapClass;
    private CustomTimer cooldownTimer;

    [HideInInspector] public float cdTime;
    private GameObject player;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        cdTime = cooldownTime;
        player = GameObject.FindGameObjectWithTag("Player");
        swapClass = player.GetComponent<SwapClass>();
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(GameManager.GM.CounterAbility))
        {
            keyDown = true;
        }
        else { keyDown = false; }

        if (keyDown) 
        {
            //If counter is usable and the update has already checked this
            if (cooldownTimer.finish && !once)
            {
                cooldownTimer.Reset();
                once = true;
            }
        }

        //Counter is usable
        if (cooldownTimer.start && once)
        {
            Counter();
            once = false;
        }

        cooldownTimer.Update();
    }

    void Counter()
    {
        //Create a temporary CounterArea
        Instantiate(CounterPrefab, new Vector3(transform.position.x, transform.position.y + playerHeight/2, transform.position.z), transform.rotation);
    }
}
