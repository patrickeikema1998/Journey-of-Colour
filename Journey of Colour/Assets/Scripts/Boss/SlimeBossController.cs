using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossController : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    bool onGround;

    [SerializeField] float jumpCooldown = 1;
    float jumpCooldownTimer = 0;

    [SerializeField] float jumpForce = 10,
                           sideForce = 5;

    bool facingLeft = true;
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

    void Jump()
    {
        m_Rigidbody.AddForce(Vector3.up * jumpForce + (facingLeft ? Vector3.left : Vector3.right) * sideForce, ForceMode.VelocityChange);
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
        }

        if (collision.gameObject.name.StartsWith("SideWall"))
        {
            facingLeft = !facingLeft;
        }
    }
}
