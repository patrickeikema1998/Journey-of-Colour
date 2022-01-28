using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CheckPoint : MonoBehaviour
{
    [HideInInspector] public float distance, time;
    float timelapsed;

    public GameObject player;
    private PlayerHealth health;
    private Rigidbody rb;
    bool checkPointHit = false;

    //[SerializeField] List<GameObject> checkPoints;

    //The respawnPos will be at y=0, but the player needs to be at y=1, so we have
    // an offset that will be the position of the player when the scene loads in.
    Vector3 respawnPos;
    Vector3 beginOffset;
    Vector3 originPos;

    PlayerMovement playerMovement;
    GameObject camera;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        health = player.GetComponent<PlayerHealth>();
        rb = player.GetComponent<Rigidbody>();
        beginOffset = Vector3.up;
        originPos = transform.position + beginOffset;
        respawnPos = originPos;

        GameEvents.onRespawnPlayer += ResetPlayerPos;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            respawnPos = other.transform.position + beginOffset;
            other.gameObject.transform.position = new Vector3(0, 10000, 0);
            checkPointHit = true;
        }
    }

    private void Update()
    {
        timelapsed += Time.deltaTime;
    }

    void ResetPlayerPos()
    {
        AutomaticScrolling scrolling = camera.GetComponent<AutomaticScrolling>();

        if (!checkPointHit) 
        {
            Analytics();
            rb.velocity = Vector3.zero;
            player.transform.position = respawnPos + beginOffset;
            if(scrolling.isActiveAndEnabled) scrolling.Reset();
        }
        else
        {
            //when the player dies and respawns at a checkpoint the playerPos will be set
            //to the respawnPos which will be acitvated when the checkPoints is triggered.
            rb.velocity = Vector3.zero;
            player.transform.position = respawnPos;
            if(scrolling.isActiveAndEnabled) scrolling.Reset();
        }
    }

    void Analytics()
    {
        distance = player.transform.position.x - originPos.x;
        time = Time.timeSinceLevelLoad - timelapsed;
        AnalyticsEvent.Custom("PlayerDeath", new Dictionary<string, object>
            {
                { "Distance",  distance},
                { "Time",  distance},
            });
        timelapsed = 0;
    }
}

//bron: https://www.youtube.com/watch?v=3CfvYnfq-9M
