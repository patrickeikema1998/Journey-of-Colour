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
        Debug.Log("COLLIDED!");
        if (collision.gameObject == target)
        {
            Debug.Log("Hit!");
            target.GetComponent<TakeDamage>().TakeHit(20);
        }

        if (collision.gameObject.tag != "Enemy")
            Destroy(gameObject);
    }
}
