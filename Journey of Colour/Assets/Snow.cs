using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    float fallSpeed = -300;
    float respawnPoint = 400f;
    [SerializeField] float sinSpeed, sinMovement;
    Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
        sinSpeed = Random.Range(1f, 1.05f);
        sinMovement = Random.Range(0.1f, 0.15f);
        fallSpeed = Random.Range(-25f, -31f);
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.fixedTime * sinSpeed) * sinMovement, transform.position.y + (fallSpeed * Time.deltaTime), transform.position.z);

        if (transform.position.y < respawnPoint) transform.position = initialPos;
    }
}
