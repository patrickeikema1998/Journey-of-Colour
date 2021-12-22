using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamAttack : MonoBehaviour
{
    bool shooting = false;
    [SerializeField]
    BeamProjectile beam;
    [SerializeField]
    LayerMask raycastLayer;
    [SerializeField]
    int damage = 10;
    [SerializeField]
    float maxAttackTime = 2;

    SlimeBossController slimeBoss;

    float attackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        slimeBoss = GetComponent<SlimeBossController>();
        BeamProjectile.maxLifeTime = maxAttackTime;
    }

    Vector3 RayDirection
    {
        get { return new Vector3((transform.position.x < 0) ? 1 : -1, 0); }
    }

    public void ShootBeam()
    {
        shooting = true;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, RayDirection, out hit, Mathf.Infinity, raycastLayer))
        {
            float beamScaleY = hit.point.x - (transform.position.x + (transform.lossyScale.x /2));
            beam.transform.localScale = new Vector3(transform.lossyScale.x, beamScaleY /2, transform.lossyScale.z);
            Instantiate(beam, transform.position + new Vector3((beamScaleY / 2) + (transform.lossyScale.x / 2 * RayDirection.x), 0), Quaternion.Euler(0, 0, 90));
        }
    }
}
