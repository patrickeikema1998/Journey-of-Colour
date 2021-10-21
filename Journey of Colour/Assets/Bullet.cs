using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody rb;

    private PlayerMovement player;

    public bool lookingLeft;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.lookingLeft = player.lookingLeft;
        if(lookingLeft) rb.velocity = transform.right * -speed;
        if (!lookingLeft) rb.velocity = transform.right * speed;
    }
}
