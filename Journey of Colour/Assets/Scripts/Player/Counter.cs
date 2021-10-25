using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float bulletForce, enemyForce, cooldownTime;
    private bool keyDown;
    private CharacterController controller;
    private CustomTimer cooldownTimer;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        cooldownTimer = new CustomTimer(cooldownTime);
        cooldownTimer.finish = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        { 
            keyDown = true;
            cooldownTimer.Reset();
            cooldownTimer.start = true;
        }

        if (cooldownTimer.finish) { keyDown = false; }
        cooldownTimer.Update();
    }

    void OnTriggerEnter(Collider c)
    {
        if (keyDown)
        {
            Vector3 dir = (c.gameObject.transform.position - transform.position).normalized;

            // If the object we hit is the enemy
            if (c.gameObject.tag == "Enemy")
            {
                float force = enemyForce;

                if (c.gameObject.GetComponent<Rigidbody>() == null) 
                {
                    this.controller = c.GetComponent<EnemyController>().controller;
                    if (controller != null)
                    {
                        c.gameObject.transform.forward = (new Vector3(
                        dir.x * c.gameObject.transform.forward.x,
                        dir.y * c.gameObject.transform.forward.y,
                        dir.z * c.gameObject.transform.forward.z)
                        );
                        controller.SimpleMove(c.gameObject.transform.forward * enemyForce);
                    }
                }
                else
                {
                    c.gameObject.GetComponent<Rigidbody>().AddForce(dir * enemyForce, ForceMode.Impulse);
                }

            }

            // If the object we hit is a bullet
            if (c.gameObject.tag == "Bullet")
            {
                float force = bulletForce;
                c.gameObject.GetComponent<Rigidbody>().AddForce(dir * bulletForce, ForceMode.Impulse);
            }

            keyDown = false;
        }
    }
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