using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPattern : MonoBehaviour
{
    float forceAmplifier = 10;
    float angleSpeedDivider = 30f;

    [SerializeField] float timeOnGround;
    [HideInInspector] public float horizontalForce, verticalForce;
    [HideInInspector] public int damage;

    Rigidbody rb;
    GameObject player;

    float angle;
    float startRotationZ, downRotationZ;
    bool onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        RotateSpearOnStart();
        angle = 0f;

        startRotationZ = transform.rotation.eulerAngles.z;
        downRotationZ = 180f;
        onGround = false;

        GoToTarget();
    }


    void Update()
    {
        if (rb.velocity.y < 0)
        {
            angle = Mathf.Lerp(startRotationZ, downRotationZ, -rb.velocity.y / angleSpeedDivider);
        }
       
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player && !onGround)
        {
            player.GetComponent<Health>().Damage(damage);
            DestroyThis();
        } else
        {
            GetComponent<BoxCollider>().isTrigger = true;
            rb.isKinematic = true;
            Invoke("DestroyThis", timeOnGround);
            onGround = true;
        }
    }

    void GoToTarget()
    {
        if (player.transform.position.x < transform.position.x) horizontalForce *= -1;

        rb.AddForce(new Vector3(horizontalForce * forceAmplifier, verticalForce * forceAmplifier, 0));
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    void RotateSpearOnStart()
    {
        if (player.transform.position.x > transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 270);
    }
}
