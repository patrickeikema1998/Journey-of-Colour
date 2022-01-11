using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAbility : MonoBehaviour
{
    [SerializeField] float counterAreaHeight, cooldownTime;
    public GameObject CounterPrefab;
    private bool keyDown, once;
    private SwapClass swapClass;
    private CustomTimer cooldownTimer;

    private GameObject player;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swapClass = player.GetComponent<SwapClass>();
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            keyDown = true;
        }
        else { keyDown = false; }

        if (keyDown && swapClass.currentClass == SwapClass.playerClasses.Devil) 
        {
            if (cooldownTimer.finish && !once)
            {
                cooldownTimer.Reset();
                once = true;
            }
        }

        if (cooldownTimer.start && once)
        {
            Counter();
            once = false;
        }

        cooldownTimer.Update();
    }

    void Counter()
    {
        Instantiate(CounterPrefab, new Vector3(transform.position.x, transform.position.y + counterAreaHeight/2, transform.position.z), transform.rotation);
    }
}
