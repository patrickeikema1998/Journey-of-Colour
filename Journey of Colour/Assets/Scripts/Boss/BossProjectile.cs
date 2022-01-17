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

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        float radians = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        m_Rigidbody.AddForce(new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)).normalized * velocity, ForceMode.VelocityChange);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
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
