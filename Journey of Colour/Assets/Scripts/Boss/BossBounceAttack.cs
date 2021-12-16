using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBounceAttack : MonoBehaviour
{
    protected Rigidbody m_Rigidbody;

    [SerializeField]
    protected Vector3 jumpVector = new Vector3(10, 25);

    [SerializeField]
    float bouncyPlatformMultiplier = 2;

    bool onGround,
         facingLeft = false,
         hitBouncy = false;

    [System.NonSerialized]
    public float jumpCooldown;

    protected float jumpCooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown) Jump(); 
        }
    }

    protected virtual void Jump()
    {
        m_Rigidbody.AddForce(Vector3.up * jumpVector.y + (facingLeft ? Vector3.left : Vector3.right) * jumpVector.x, ForceMode.VelocityChange);
        jumpCooldownTimer = 0;
        
    }
    private void OnCollisionExit(Collision collision)
    {
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
            if (hitBouncy)
            {
                hitBouncy = false;
                GetComponent<SlimeBossController>().Stun();
            }
        }
        if (collision.gameObject.name.StartsWith("SideWall")) facingLeft = !facingLeft;
        else if (collision.gameObject.name.Contains("Bounce"))
        {
            m_Rigidbody.AddForce((Vector3.up * jumpVector.y + (facingLeft ? Vector3.left : Vector3.right) * jumpVector.x) * bouncyPlatformMultiplier, ForceMode.VelocityChange);
            hitBouncy = true;
        }
    }
}
