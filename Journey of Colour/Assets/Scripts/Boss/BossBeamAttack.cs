using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamAttack : MonoBehaviour
{
    bool shooting = false;
    [SerializeField]
    BeamProjectile beam;
    [SerializeField]
    int damage = 10;
    [SerializeField]
    float maxAttackTime = 2;

    float attackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        BeamProjectile.maxLifeTime = maxAttackTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (shooting)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + new Vector3(0, transform.lossyScale.y / 2), RayDirection, out hit, Mathf.Infinity))
            {
                CheckHit(hit);
            }
            if (Physics.Raycast(transform.position, RayDirection, out hit, Mathf.Infinity))
            {
                CheckHit(hit);
            }
            if (Physics.Raycast(transform.position - new Vector3(0, transform.lossyScale.y / 2), RayDirection, out hit, Mathf.Infinity))
            {
                CheckHit(hit);
            }
            attackTime += Time.deltaTime;
            if (attackTime >= maxAttackTime)
            {
                shooting = false;
                attackTime = 0;
            }
        }
    }

    Vector3 RayDirection
    {
        get { return new Vector3((transform.position.x < 0) ? 1 : -1, 0); }
    }

    void CheckHit(RaycastHit hit)
    {
        if (hit.collider != null && hit.collider.GetComponent<Health>() != null) hit.collider.GetComponent<Health>().Damage(damage);
    }

    public void ShootBeam()
    {
        if (!shooting)
        {
            shooting = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, RayDirection, out hit, Mathf.Infinity))
            {
                float beamScaleY = hit.point.x - (transform.position.x + (transform.lossyScale.x /2));
                beam.transform.localScale = new Vector3(transform.lossyScale.x, beamScaleY /2, transform.lossyScale.z);
                Instantiate(beam, transform.position + new Vector3((beamScaleY / 2) + (transform.lossyScale.x / 2 * RayDirection.x), 0), Quaternion.Euler(0, 0, 90));
            }
        }
    }
}
