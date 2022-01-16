using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    float deathHeight = -12f;
    private void Update()
    {
        if (transform.position.y < deathHeight) GameEvents.RespawnPlayer();
    }
}
