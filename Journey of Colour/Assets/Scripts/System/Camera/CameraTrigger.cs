using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject camera;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        { 
            camera.GetComponent<AutomaticScrolling>().moving = false;
            camera.GetComponent<AutomaticScrolling>().Triggered(transform.position);
        }
    }
}
