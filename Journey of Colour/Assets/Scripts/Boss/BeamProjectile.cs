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
        //dissapears after set time
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime) Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        //only damages the player
        PlayerHealth otherHealth = other.GetComponent<PlayerHealth>();
        if (otherHealth != null && other.GetComponent<SlimeBossController>() == null) otherHealth.Damage(damage);
    }
}
