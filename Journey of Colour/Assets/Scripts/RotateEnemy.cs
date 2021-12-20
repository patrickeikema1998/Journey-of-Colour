using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    float left, right;
    [SerializeField] float interpolationSteps;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        left = 270;
        right = 90;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToPlayer();
    }

    void RotateToPlayer()
    {
        if (player.transform.position.x > transform.position.x) transform.rotation = Quaternion.Euler(0, Mathf.Lerp(transform.rotation.eulerAngles.y, right, interpolationSteps), 0);
        else transform.rotation = Quaternion.Euler(0, Mathf.Lerp(transform.rotation.eulerAngles.y, left, interpolationSteps), 0);
    }
}
