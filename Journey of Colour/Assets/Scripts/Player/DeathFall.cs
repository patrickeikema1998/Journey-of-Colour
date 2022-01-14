using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerHealth health;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathFall")
        {
            health.Damage(1000);
        }
    }
}
