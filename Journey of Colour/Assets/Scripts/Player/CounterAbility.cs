using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAbility : MonoBehaviour
{
    [SerializeField] float thrownObjectForce, stationaryEnemyForce;
    private bool keyDown;
    private CharacterController controller;
    private SwapClass swapClass;

    private GameObject player;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swapClass = player.GetComponent<SwapClass>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            keyDown = true;
        }

        if (keyDown && swapClass.currentClass == SwapClass.playerClasses.Devil ) 
        { Counter(); }
    }

    void Counter()
    {

    }
}
