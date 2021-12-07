using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject player;
    private Health health;
    private Rigidbody rb;
    bool checkPointHit = false;

    [SerializeField] List<GameObject> checkPoints;

    //The respawnPos will be at y=0, but the player needs to be at y=1, so we have
    // an offset that will be the position of the player when the scene loads in.
    Vector3 respawnPos;
    Vector3 beginOffset;

    void Start()
    {
        health = player.GetComponent<Health>();
        rb = player.GetComponent<Rigidbody>();
        beginOffset = new Vector3(0, 1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0 && checkPointHit == false)
        {
            player.transform.position = respawnPos + beginOffset;
        }
        else if (health.health <= 0 && checkPointHit)
        {
            //when the player dies and respawns at a checkpoint the playerPos will be set
            //to the respawnPos which will be acitvated when the checkPoints is triggered.
            player.transform.position = respawnPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        respawnPos = player.transform.position;

        if(other.gameObject.layer == 8) Destroy(other.gameObject);

        checkPointHit = true;
    }
}

//bron: https://www.youtube.com/watch?v=3CfvYnfq-9M
