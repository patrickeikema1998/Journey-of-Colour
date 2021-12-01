using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20;
    public int damage = 5;

    public Rigidbody rb;
    public GameObject player;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();

        rb.velocity = transform.right * speed;
    }

    // When this gameObject is ouside the screen, it will be destroyed.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // If this gameObject collides with an object with the tag "Enemy", the
    // enemy will take 5 damage from within the HealthClass on the enemy itself.
    // If it collides with something else, it will simply get destroyed
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().Damage(damage);
        }
        Destroy(gameObject);
    }
}
