using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehavior : MonoBehaviour
{
    //force amplifier exists so that editor numbers arent crazy high.
    const float forceAmplifier = 10;
    const float angleSpeedDivider = 30f;

    [SerializeField] float timeOnGround;
    [HideInInspector] public float horizontalForce, verticalForce;
    [HideInInspector] public int damage; 
    private int enemyDamage = 5;

    Rigidbody rb;
    GameObject player;

    float angle;
    float startRotationZ, downRotationZ;
    public bool onGround;
    public bool deflected;
    private bool spearDeflected;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        onGround = false;

        RotateSpearOnStart();
        startRotationZ = transform.rotation.eulerAngles.z;
        downRotationZ = 180f;

        MoveSpear();
    }


    void Update()
    {
        if (!deflected)
        {
            RotateSpear();
        }
    }

    //rotates spear based on y velocity.
    private void RotateSpear()
    {
        if (rb.velocity.y < 0)
        {
            angle = Mathf.Lerp(startRotationZ, downRotationZ, -rb.velocity.y / angleSpeedDivider);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MoveSpear()
    {
        //checks for player location and adjusts horizontal direction
        if (player.transform.position.x < transform.position.x) horizontalForce *= -1;

        //moves the spear.
        rb.AddForce(new Vector3(horizontalForce * forceAmplifier, verticalForce * forceAmplifier, 0));
    }


    private void OnCollisionEnter(Collision collision)
    {
        switch (onGround)
        {
            case true:
                break;
            case false:
                switch (collision.gameObject.tag)
                {
                    case "Player":
                        player.GetComponent<PlayerHealth>().Damage(damage);
                        DestroyThis();
                        break;
                    case "Enemy":
                        collision.gameObject.GetComponent<EnemyHealth>().Damage(enemyDamage);
                        DestroyThis();
                        break;
                    case "Bullet":
                        if (!spearDeflected)
                        {
                            Deflect();
                            spearDeflected = true;
                            deflected = true;
                        }
                        break;
                }
                GetComponent<BoxCollider>().isTrigger = true;
                rb.isKinematic = true;
                Invoke("DestroyThis", timeOnGround);
                onGround = true;
                break;
            default:
                Debug.Log("onGround is empty");
                break;
        }

        ////if colliding with player and spear not on the ground, hit the player and destroy spear.
        //if (collision.gameObject == player && !onGround)
        //{
        //    player.GetComponent<Health>().Damage(damage);
        //    DestroyThis();
        //}
        //else if (collision.gameObject.tag == "Enemy" && !onGround)
        //{
        //    collision.gameObject.GetComponent<Health>().Damage(enemyDamage);
        //    DestroyThis();
        //}
        ////if colliding with another spear
        //else if (collision.gameObject.tag == "Bullet" && !spearDeflected)
        //{
        //    Deflect();
        //    spearDeflected = true;
        //    deflected = true;
        //}
        ////if colliding with anything else than player and spear is not on the ground, stop spear from colliding and moving and destroy it after some time.
        //else if (!onGround)
        //{
        //    GetComponent<BoxCollider>().isTrigger = true;
        //    rb.isKinematic = true;
        //    Invoke("DestroyThis", timeOnGround);
        //    onGround = true;
        //}
    }

    //this only exists for invokes.
    void Deflect()
    {transform.rotation = new Quaternion(
            transform.rotation.x,
            -transform.rotation.y,
            transform.rotation.z,
            -transform.rotation.w
        );
    }    
    
    //this only exists for invokes.
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    //rotate the spear towards the player
    void RotateSpearOnStart()
    {
        if (player.transform.position.x > transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 270);
    }
}
