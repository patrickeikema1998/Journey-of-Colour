using UnityEngine;

public class SlimeBossController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Health health;
    EnemyShoot rangedAttack;

    bool onGround;
    int phase = 1;

    [SerializeField] int meleeDamage = 4;

    [SerializeField] float jumpCooldownPhase1 = 1, jumpCooldownPhase2 = 1f, jumpCooldownPhase3 = 0.5f,
                           shootCooldownPhase2 = 2, shootCooldownPhase3 = 1,
                           burstTime = 1.6f;
    float jumpCooldown;
    float jumpCooldownTimer = 0;

    [SerializeField] float jumpForce = 10,
                           sideForce = 5;


    bool facingLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        rangedAttack = GetComponent<EnemyShoot>();
        SwitchPhase(1);
    }

    // Update is called once per frame
    void Update()
    {
        //jumpen doet hij altijd de rest hang af van welke phase
        if (onGround)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown) Jump();
        }
        switch (phase)
        {
            case 1:
                if (health.GetHealth < health.maxHealth / 3 * 2) SwitchPhase(phase + 1);  
                break;
            case 2:
                if (health.GetHealth < health.maxHealth / 3) SwitchPhase(phase + 1);
                break;
            case 3:
                if (health.dead) /*death sequence*/Destroy(gameObject);
                break;
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
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(meleeDamage);
        }
        if (collision.gameObject.name.StartsWith("SideWall")) facingLeft = !facingLeft; 
        
    }

    void SwitchPhase(int newPhase)
    {
        switch (newPhase)
        {
            case 1:
                rangedAttack.enabled = false;
                jumpCooldown = jumpCooldownPhase1;
                break;
            case 2:
                InvokeRepeating("InvokeRangedAttack", shootCooldownPhase2, shootCooldownPhase2 + burstTime);
                jumpCooldown = jumpCooldownPhase2;
                break;
            case 3:
                CancelInvoke("InvokeRangedAttack");
                EndRangedAttack();
                InvokeRepeating("InvokeRangedAttack", shootCooldownPhase3, shootCooldownPhase3 + burstTime);
                jumpCooldown = jumpCooldownPhase3;
                break;
        }
        phase = newPhase;
    }
    void InvokeRangedAttack()
    {
        rangedAttack.enabled = true;
        Invoke("EndRangedAttack", burstTime);
    }

    void EndRangedAttack()
    {
        rangedAttack.enabled = false;
    }
}
