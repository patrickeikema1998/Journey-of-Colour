using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float thrownObjectForce, stationaryEnemyForce, counterDistance;
    private bool keyDown;
    private CharacterController controller;
    private SwapClass swapClass;

    private GameObject player;
    private float distance;
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

        distance = Vector3.Distance(transform.position, player.transform.position);
        if (keyDown) { Redirect(); }
    }

    void Redirect()
    {

        if (keyDown && swapClass.currentClass == SwapClass.playerClasses.Devil && distance <= counterDistance)
        {
            Vector3 dir = (transform.position - player.transform.position).normalized;

            // If the object we hit is the enemy
            if (gameObject.tag == "Enemy")
            {
                if (GetComponent<Rigidbody>() == null)
                {
                    this.controller = GetComponent<EnemyController>().controller;
                    if (controller != null)
                    {
                        if (dir.x == 0) { dir.x = 1; }
                        transform.forward = (new Vector3(
                        dir.x,
                        dir.y,
                        dir.z)
                        );

                        controller.SimpleMove(transform.forward * stationaryEnemyForce);
                    }
                }
                else
                {
                    if (dir.x == 0) { dir.x = 1; }
                    dir = new Vector3(dir.x, dir.y, dir.z);

                    GetComponent<Rigidbody>().AddForce(dir * stationaryEnemyForce, ForceMode.Impulse);
                }

            }

            // If the object we hit is a bullet
            if (gameObject.tag == "Bullet")
            {
                if (dir.x == 0) { dir.x = 1; }
                GetComponent<Rigidbody>().AddForce(dir * thrownObjectForce, ForceMode.Impulse);
            }
            keyDown = false;
        }
    }
    //void OnTriggerEnter(Collider c)
    //{
    //    Debug.Log("SAVE ME");
    //    if (keyDown && swapClass.currentClass == SwapClass.playerClasses.Devil)
    //    {
    //        Vector3 dir = (c.gameObject.transform.position - transform.position).normalized;


    //        // If the object we hit is the enemy
    //        if (c.gameObject.tag == "Enemy")
    //        {
    //            float force = stationaryEnemyForce;

    //            if (c.gameObject.GetComponent<Rigidbody>() == null)
    //            {
    //                this.controller = c.GetComponent<EnemyController>().controller;
    //                if (controller != null)
    //                {
    //                    c.gameObject.transform.forward = (new Vector3(
    //                    dir.x,
    //                    dir.y,
    //                    dir.z)
    //                    );
    //                    controller.SimpleMove(c.gameObject.transform.forward * stationaryEnemyForce);
    //                }
    //            }
    //            else
    //            {
    //                c.gameObject.GetComponent<Rigidbody>().AddForce(dir * stationaryEnemyForce, ForceMode.Impulse);
    //            }

    //        }

    //        // If the object we hit is a bullet
    //        if (c.gameObject.tag == "Bullet")
    //        {
    //            float force = thrownObjectForce;
    //            c.gameObject.GetComponent<Rigidbody>().AddForce(dir * thrownObjectForce, ForceMode.Impulse);
    //        }

    //        keyDown = false;
    //    }
    //}
}
//// If the object we hit is the enemy
//if (c.gameObject.tag == "Enemy")
//{
//    // force is how forcefully we will push the player away from the enemy.
//    float force = 6;
//    // Calculate Angle Between the collision point and the player
//    Vector3 dir = c.contacts[0].point - transform.position;
//    // We then get the opposite (-Vector3) and normalize it
//    dir = -dir.normalized;
//    // And finally we add force in the direction of dir and multiply it by force. 
//    // This will push back the player
//    GetComponent<Rigidbody>().AddForce(dir * force);
//}