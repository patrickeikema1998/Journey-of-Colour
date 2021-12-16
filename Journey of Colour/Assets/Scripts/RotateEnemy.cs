using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    Quaternion left, right;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        left = Quaternion.Euler(0, 270, 0);
        right = Quaternion.Euler(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToPlayer();
    }

    void RotateToPlayer()
    {
        if (player.transform.position.x > transform.position.x) transform.rotation = right;
        else transform.rotation = left;
    }
}
