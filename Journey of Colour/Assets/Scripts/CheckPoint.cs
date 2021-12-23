using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject player;
    private Health health;
    private Rigidbody rb;
    bool checkPointHit = false;

    //[SerializeField] List<GameObject> checkPoints;

    //The respawnPos will be at y=0, but the player needs to be at y=1, so we have
    // an offset that will be the position of the player when the scene loads in.
    Vector3 respawnPos;
    Vector3 beginOffset;
    Vector3 originPos;

    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        health = player.GetComponent<Health>();
        rb = player.GetComponent<Rigidbody>();
        beginOffset = new Vector3(0, 1, 0);
        originPos = transform.position + beginOffset;

        GameEvents.onRespawnPlayer += ResetPlayerPos;
    }
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            respawnPos = originPos;
            gameObject.transform.position = new Vector3(0, 10000, 0);
            checkPointHit = true;
        }
    }

    void ResetPlayerPos()
    {
        if (!checkPointHit) player.transform.position = respawnPos + beginOffset;
        else
        {
            //when the player dies and respawns at a checkpoint the playerPos will be set
            //to the respawnPos which will be acitvated when the checkPoints is triggered.
            player.transform.position = respawnPos;
        }
    }
}

//bron: https://www.youtube.com/watch?v=3CfvYnfq-9M
