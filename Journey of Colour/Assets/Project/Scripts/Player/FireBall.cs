using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public int damage = 5;

    public Rigidbody rb;
    public GameObject player;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(ObjectTags._PlayerTag);
        playerMovement = player.GetComponent<PlayerMovement>();

        rb.velocity = transform.right * speed;
    }

    // When this gameObject is ouside the screen, it will be destroyed.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // If this gameObject collides with an object with the tag "Enemy", the
    // enemy will take damage from within the EnemyHealth on the enemy itself.
    // If the FireBall collides with the camTrigger of another bullet, it will simply get destroyed.
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(ObjectTags._EnemyTag))
        {
            collision.GetComponent<EnemyHealth>().Damage(damage);
        }
        if (collision.CompareTag(ObjectTags._CamTriggerTag) && collision.CompareTag(ObjectTags._BulletTag)) Destroy(gameObject);
    }
}
