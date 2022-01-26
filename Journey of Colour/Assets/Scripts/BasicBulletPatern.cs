using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletPatern : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float playerOffsetY;
    [SerializeField] int damage;
    private GameObject target;
    private Rigidbody rb;
    private Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        moveDirection = new Vector3(moveDirection.x, moveDirection.y + playerOffsetY, moveDirection.z);
        rb.velocity = moveDirection;
    }

    //fuction that makes the projectile dissapear
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //does damage to either player or enemy
        if (collision.gameObject == target)
            target.GetComponent<PlayerHealth>().Damage(damage);

        else if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyHealth>().Damage(damage);

        OnBecameInvisible();
    }
}
