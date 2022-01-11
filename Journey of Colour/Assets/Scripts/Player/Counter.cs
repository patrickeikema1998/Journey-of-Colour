using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float thrownObjectForce, stationaryEnemyForce, counterAreaHeight, fadeTime;
    private CharacterController controller;
    private CustomTimer fadeTimer;

    private GameObject player;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fadeTimer = new CustomTimer(fadeTime);
    }
    private void Update()
    {
        if (fadeTimer.finish) 
        {
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet") 
        { 
            Redirect(other);
            fadeTimer.start = true;
        }
        else { }
    }

    void Redirect(Collider collider)
    {
        GameObject gameObject = collider.gameObject;
        Debug.Log(gameObject.tag);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (counterAreaHeight/2), player.transform.position.z);
        Vector3 dir = (gameObject.transform.position - player.transform.position).normalized;

        // If the object we hit is the enemy
        if (gameObject.tag == "Enemy")
        {
            if (GetComponent<Rigidbody>() == null)
            {
                //controller = gameObject.GetComponent<CharacterController>();
                controller = gameObject.GetComponent<EnemyController>().controller;
                if (controller != null)
                {
                    if (dir.x == 0) { dir.x = 1; }
                    transform.forward = (new Vector3(
                    dir.x,
                    dir.y,
                    dir.z)
                    );

                    controller.SimpleMove(gameObject.transform.forward * stationaryEnemyForce);
                }
            }
            else
            {
                if (dir.x == 0) { dir.x = 1; }
                dir = new Vector3(dir.x, dir.y, dir.z);

                gameObject.GetComponent<Rigidbody>().AddForce(dir * stationaryEnemyForce, ForceMode.Impulse);
            }

        }

        // If the object we hit is a bullet
        if (gameObject.tag == "Bullet")
        {
            if (dir.x == 0) { dir.x = 1; }
            gameObject.GetComponent<Rigidbody>().AddForce(dir * thrownObjectForce, ForceMode.Impulse);
        }
    }
}