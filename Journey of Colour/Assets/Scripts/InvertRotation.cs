using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertRotation : MonoBehaviour
{
    GameObject enemy;

    private void Start()
    {
        enemy = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, enemy.transform.rotation.y, 0, 0);
    }
}
