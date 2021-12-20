using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] float bounceValue = 1;


    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //bounces the colliding object
            collision.rigidbody.velocity = transform.up * bounceValue;
        }
    }
}
