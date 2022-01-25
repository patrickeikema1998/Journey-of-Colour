using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamAttack : MonoBehaviour
{
    float originalBeamScale = 13;

    bool shooting = false;

    [SerializeField]
    BeamProjectile beam;

    [SerializeField]
    LayerMask raycastLayer;

    [SerializeField]
    float maxAttackTime = 2;

    float attackTime = 0;

    SlimeBossController slimeBoss;

    // Start is called before the first frame update
    void Start()
    {
        BeamProjectile.maxLifeTime = maxAttackTime;
        slimeBoss = GetComponent<SlimeBossController>();
    }

    // Update is called once per frame
    void Update()
    {
        //timers
        if (shooting)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= maxAttackTime)
            {
                shooting = false;
                attackTime = 0;
            }
        }
    }

    public Vector3 RayDirection
    {
        //the direction in which the beam has to shoot
        get { return new Vector3((transform.position.x < 0) ? 1 : -1, 0); }
    }

    public void ShootBeam()
    {
        if (!shooting)
        {
            shooting = true;

            //spawns the beam and scales it to reach the other end of the stage
            RaycastHit hit;
            if (Physics.Raycast(transform.position, RayDirection, out hit, Mathf.Infinity, raycastLayer))
            {
                float beamScaleY = hit.point.x - (transform.position.x + (transform.lossyScale.x /2));
                beam.transform.localScale = new Vector3(transform.lossyScale.x, beamScaleY /2, transform.lossyScale.z);
                Instantiate(beam, transform.position + new Vector3((beamScaleY / 2) + (transform.lossyScale.x / 2 * RayDirection.x), 0), Quaternion.Euler(0, 0, 90));
                beam.transform.localScale = new Vector3(transform.lossyScale.x, originalBeamScale, transform.lossyScale.z);

                slimeBoss.chargingParticles.Stop();

                Invoke("StunBoss", maxAttackTime);
            }
        }
    }

    void StunBoss()
    {
        //stuns the boss
        slimeBoss.Stun();
    }
}
