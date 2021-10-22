using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody rb;

    public GameObject player;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();

        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().Damage(5);
        }
        Destroy(gameObject);
    }
}
