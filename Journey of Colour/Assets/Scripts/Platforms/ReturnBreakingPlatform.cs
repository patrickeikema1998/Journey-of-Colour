using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBreakingPlatform : MonoBehaviour
{
    [SerializeField] GameObject breakingPlatform;
    [HideInInspector]public bool go;
    CustomTimer respawnTimer;
    [SerializeField] float secondsToRespawn;
    // Start is called before the first frame update
    void Start()
    {
        respawnTimer = new CustomTimer(secondsToRespawn);
    }

    // Update is called once per frame
    void Update()
    {
        respawnTimer.Update();
        if (go)
        {
            respawnTimer.start = true;
            go = false;
        }

        if (respawnTimer.finish)
        {
            Instantiate(breakingPlatform, transform);
            respawnTimer.Reset();
            respawnTimer.start = false;
        }
    }
}
