using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find(ObjectTags._PlayerTag);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.transform.position - transform.position);
        direction.y = 0;
        direction.Normalize();

        //rotates object to face the player
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
