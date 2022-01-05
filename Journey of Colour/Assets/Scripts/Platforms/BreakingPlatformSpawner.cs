using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject breakingPlatform;
    [HideInInspector]public bool go;
    public CustomTimer respawnTimer;
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
        //when timer is finished, respawns a breaking platform on the same location.
        if (respawnTimer.finish)
        {
            Instantiate(breakingPlatform, transform);
            respawnTimer.Reset();
            respawnTimer.start = false;
        }
    }
}
