using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletPatern : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    GameObject target;
    Rigidbody rb;
    Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = moveDirection;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            //Debug.Log("Hit!");
        }

        if (collision.gameObject.tag != "Enemy")
            Destroy(gameObject);
         
    }
}
