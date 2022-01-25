using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    [SerializeField]
    float velocity;

    [SerializeField]
    int damage;

    [SerializeField]
    float maxLifeTime;

    float lifeTime;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        //give the projectile the right angle and speed
        float radians = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        m_Rigidbody.AddForce(new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)).normalized * velocity, ForceMode.VelocityChange);
    }

    private void Update()
    {
        //the projectile dissapears after a set amount of time
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //checks what it has collided with and destroys itself
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Damage(damage);
            Destroy(gameObject);
        }
        if (!other.CompareTag("Enemy") && !other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
