using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAbility : MonoBehaviour
{
    [SerializeField] float thrownObjectForce, stationaryEnemyForce, counterAreaHeight, cooldownTime;
    public GameObject CounterPrefab;
    private bool keyDown;
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
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            keyDown = true;
        }

        if (keyDown && swapClass.currentClass == SwapClass.playerClasses.Devil ) 
        { 
            Counter();
            cooldownTimer.start = true;
        }

        if (cooldownTimer.finish) { cooldownTimer.Reset(); }
    }

    void Counter()
    {
        Instantiate(CounterPrefab, new Vector3(transform.position.x, transform.position.y + counterAreaHeight/2, transform.position.z), transform.rotation);
        keyDown = false;
    }
}
