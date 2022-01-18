using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLungeAttack : BossBounceAttack
{
    [SerializeField] 
    GameObject player;

    [SerializeField]
    float lungeForce = 15;

    protected override void Jump()
    {
        //adds partially randomized force up and towards player
        float jumpRandomizer = Random.Range(jumpRandomizerRange.x, jumpRandomizerRange.y);

        m_Rigidbody.AddForce((PlayerDirection + (jumpVector * jumpRandomizer)).normalized * lungeForce * jumpRandomizer, ForceMode.VelocityChange);
        jumpCooldownTimer = 0;
    }

    Vector3 PlayerDirection
    {
        //returns direction of player from boss
        get { return (player.transform.position - transform.position).normalized; }
    }
}
