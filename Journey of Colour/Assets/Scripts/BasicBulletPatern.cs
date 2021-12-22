using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletPatern : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float playerOffsetY;
    GameObject target;
    Rigidbody rb;
    Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        moveDirection = new Vector3(moveDirection.x, moveDirection.y + playerOffsetY, moveDirection.z);
        rb.velocity = moveDirection;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            //Debug.Log("Hit!");
            //target.GetComponent<TakeDamage>().TakeHit(2);
            target.GetComponent<Health>().Damage(15);
        }

        Destroy(gameObject);

        //if (collision.gameObject.tag != "Enemy")
        //    Destroy(gameObject);
    }
}
