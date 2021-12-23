using UnityEngine;

public class SlimeBossController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Health health;
    BossBounceAttack bounceAttack;
    BossLungeAttack lungeAttack;
    BossProjectileAttack projectileAttack;
    BossBeamAttack beamAttack;

    [SerializeField]
    int phase = 1;

    [SerializeField] 
    int meleeDamage = 4;

    [SerializeField]
    float jumpCooldownPhase1 = 1, jumpCooldownPhase3 = 0.5f,
          lungeCooldownPhase2 = 2, lungeCooldownPhase4 = 1.5f,
          shootCooldownPhase3 = 2, shootCooldownPhase4 = 2;

    [SerializeField]
    float maxStunTime = 4;
    float stunTime;
    bool stunned;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        bounceAttack = GetComponent<BossBounceAttack>();
        lungeAttack = GetComponent<BossLungeAttack>();
        projectileAttack = GetComponent<BossProjectileAttack>();
        beamAttack = GetComponent<BossBeamAttack>();
        SwitchPhase(phase);
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            stunTime += Time.deltaTime;
            if (stunTime >= maxStunTime) SwitchPhase(phase);
        }

        switch (phase)
        {
            case 1:
                if (health.GetHealth < health.maxHealth / 4 * 3) SwitchPhase(phase + 1);  
                break;
            case 2:
                if (health.GetHealth < health.maxHealth / 4 * 2) SwitchPhase(phase + 1);
                break;
            case 3:
                if (health.GetHealth < health.maxHealth / 4) SwitchPhase(phase + 1);
                break;
            case 4:
                if (health.dead) /*death sequence*/Destroy(gameObject);
                break;
        }
        
    }

    public void SpikesHit()
    {
        if (phase < 4) Stun();
        else
        {
            Stun();
            Invoke("ShootBeam", maxStunTime);
        }
    }

    public void Stun()
    {
        stunTime = 0;
        stunned = true;
        bounceAttack.enabled = false;
        lungeAttack.enabled = false;
        projectileAttack.enabled = false;
        beamAttack.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {     
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(meleeDamage);
        }
    }

    void SwitchPhase(int newPhase)
    {
        switch (newPhase)
        {
            case 1:
                bounceAttack.enabled = true;
                bounceAttack.jumpCooldown = jumpCooldownPhase1;
                lungeAttack.enabled = false;
                projectileAttack.enabled = false;
                beamAttack.enabled = false;
                stunned = false;
                break;
            case 2:
                bounceAttack.enabled = false;
                lungeAttack.enabled = true;
                lungeAttack.jumpCooldown = lungeCooldownPhase2;
                projectileAttack.enabled = false;
                beamAttack.enabled = false;
                stunned = false;
                break;
            case 3:
                bounceAttack.enabled = true;
                bounceAttack.jumpCooldown = jumpCooldownPhase3;
                lungeAttack.enabled = false;
                projectileAttack.enabled = true;
                projectileAttack.shootCooldown = shootCooldownPhase3;
                projectileAttack.burstEnabled = true;
                beamAttack.enabled = false;
                stunned = false;
                break;
            case 4:
                bounceAttack.enabled = false;
                lungeAttack.enabled = true;
                lungeAttack.jumpCooldown = lungeCooldownPhase4;
                projectileAttack.enabled = true;
                projectileAttack.shootCooldown = shootCooldownPhase4;
                projectileAttack.burstEnabled = false;
                beamAttack.enabled = true;
                stunned = false;
                break;
        }
        phase = newPhase;
    }
    
    void ShootBeam()
    {
        beamAttack.ShootBeam();
    }
    
}
