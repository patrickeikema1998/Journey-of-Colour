using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathFall")
        {
            health.Damage(100);
        }
    }
}
