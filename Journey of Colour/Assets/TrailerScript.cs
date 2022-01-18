using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerScript : MonoBehaviour
{
    public Vector3 posOffset;
    private Vector3 camMovement;

    private void Start()
    {
        transform.position = posOffset;
    }

    private void Update()
    {
        transform.position += camMovement;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            camMovement = new Vector3(0.1f, 0, 0);
        }
    }
}
