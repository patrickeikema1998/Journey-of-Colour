using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBounceAttack : MonoBehaviour
{
    protected Rigidbody m_Rigidbody;

    [SerializeField]
    protected Vector3 jumpVector = new Vector3(10, 25);
    [SerializeField]
    protected Vector2 jumpRandomizerRange = new Vector2(0, 5);

    [SerializeField]
    float bouncyPlatformMultiplier = 2;

    bool onGround,
         facingLeft = false,
         hitBouncy = false;

    [System.NonSerialized]
    public static bool bouncyPlatformStuns = false;

    [System.NonSerialized]
    public float jumpCooldown;

    protected float jumpCooldownTimer = 0;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        //jumps if on the ground and after timer
        if (onGround)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown) Jump(); 
        }
    }

    protected virtual void Jump()
    {
        //adds a partially randomized force upward
        float xRandomizer = Random.Range(jumpRandomizerRange.x, jumpRandomizerRange.y);

        m_Rigidbody.AddForce(Vector3.up * jumpVector.y + (facingLeft ? Vector3.left : Vector3.right) * (jumpVector.x + xRandomizer), ForceMode.VelocityChange);
        jumpCooldownTimer = 0;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        //handles onGround bool
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            jumpCooldownTimer = 0;
            //handles stun after collision with bouncy platform
            if (hitBouncy)
            {
                hitBouncy = false;
                GetComponent<SlimeBossController>().Stun();
            }
        }
        //switches directions if it hits the sidewall
        if (collision.gameObject.name.StartsWith("SideWall")) facingLeft = !facingLeft;
        else if (bouncyPlatformStuns && collision.gameObject.name.Contains("Bounce"))
        {
            //adds extra bounce when it hits a bouncy platform
            m_Rigidbody.AddForce((Vector3.up * jumpVector.y + (facingLeft ? Vector3.left : Vector3.right) * jumpVector.x) * bouncyPlatformMultiplier, ForceMode.VelocityChange);
            hitBouncy = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //handles onGround bool
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
