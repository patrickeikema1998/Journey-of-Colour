using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    CustomTimer lavaTimer;

    private void Start()
    {
        lavaTimer = new CustomTimer(0.5f);
        lavaTimer.start = true;
    }

    private void Update()
    {
        lavaTimer.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HOI");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity *= 0.7f;
        }

        if (lavaTimer.finish && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().Damage(30);
            lavaTimer.Reset();
        }

    }
}
