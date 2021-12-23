using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamProjectile : MonoBehaviour
{
    [System.NonSerialized]
    public static float maxLifeTime;
    float lifeTime;

    [SerializeField]
    int damage = 1;

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime) Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth != null) otherHealth.Damage(damage);
    }
}
