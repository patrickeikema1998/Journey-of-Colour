using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class PhaseEvent : UnityEvent<int> { }

public class SlimeBossController : MonoBehaviour
{
    BossHealth health;
    BossBounceAttack bounceAttack;
    BossLungeAttack lungeAttack;
    BossProjectileAttack projectileAttack;
    BossBeamAttack beamAttack;

    [System.NonSerialized] public ParticleSystem stunParticles;
    [System.NonSerialized] public ParticleSystem chargingParticles;

    //controls the phase outside of the slimeboss object
    public static PhaseEvent PhaseChange = new PhaseEvent();

    Vector3 startPosition;

    [SerializeField]
    public int phase = 1;

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
        startPosition = transform.position;
        health = GetComponent<BossHealth>();
        stunParticles = GetComponentsInChildren<ParticleSystem>()[0];
        chargingParticles = GetComponentsInChildren<ParticleSystem>()[1];
        bounceAttack = GetComponent<BossBounceAttack>();
        lungeAttack = GetComponent<BossLungeAttack>();
        projectileAttack = GetComponent<BossProjectileAttack>();
        beamAttack = GetComponent<BossBeamAttack>();
        SwitchPhase(phase);
        GameEvents.onRespawnPlayer += ResetBossFight;
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
            //checks the health and switches phase
            case 1:
                if (health.GetHealth <= health.maxHealth / 4 * 3) SwitchPhase(phase + 1);
                break;
            case 2:
                if (health.GetHealth <= health.maxHealth / 4 * 2) SwitchPhase(phase + 1);
                break;
            case 3:
                if (health.GetHealth <= health.maxHealth / 4) SwitchPhase(phase + 1);
                break;
            case 4:
                if (health.dead) gameObject.SetActive(false);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //damages the player on contact
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(meleeDamage);
        }
    }

    public void SpikesHit()
    {
        if (phase < 4) Stun();
        else
        {
            Stun();
            //changes the direction of the particles
            var chargingParticlesShape = chargingParticles.shape;
            chargingParticlesShape.scale = new Vector3(chargingParticles.shape.scale.x, chargingParticles.shape.scale.y, beamAttack.RayDirection.x);
            chargingParticlesShape.position = new Vector3(chargingParticles.shape.position.x * beamAttack.RayDirection.x, chargingParticles.shape.position.y, chargingParticles.shape.position.z);
            
            chargingParticles.Play();
            Invoke("Stun", maxStunTime);
            Invoke("ShootBeam", maxStunTime);
        }
    }

    public void Stun()
    {
        //dissables all of the attacks
        stunTime = 0;
        stunned = true;
        bounceAttack.enabled = false;
        lungeAttack.enabled = false;
        projectileAttack.enabled = false;
        beamAttack.enabled = false;
        stunParticles.Clear();
        stunParticles.Play();
    }

    void ShootBeam()
    {
        beamAttack.enabled = true;
        beamAttack.ShootBeam();
    }

    void SwitchPhase(int newPhase)
    {
        gameObject.SetActive(true);
        //enables / disables the attacks depending on the phase it switches to
        switch (newPhase)
        {
            case 1:
                bounceAttack.enabled = true;
                bounceAttack.jumpCooldown = jumpCooldownPhase1;
                BossBounceAttack.bouncyPlatformStuns = true;
                lungeAttack.enabled = false;
                projectileAttack.enabled = false;
                beamAttack.enabled = false;
                break;
            case 2:
                bounceAttack.enabled = false;
                lungeAttack.enabled = true;
                BossBounceAttack.bouncyPlatformStuns = false;
                lungeAttack.jumpCooldown = lungeCooldownPhase2;
                projectileAttack.enabled = false;
                beamAttack.enabled = false;
                break;
            case 3:
                bounceAttack.enabled = true;
                bounceAttack.jumpCooldown = jumpCooldownPhase3;
                BossBounceAttack.bouncyPlatformStuns = false;
                lungeAttack.enabled = false;
                projectileAttack.enabled = true;
                projectileAttack.shootCooldown = shootCooldownPhase3;
                projectileAttack.burstEnabled = true;
                beamAttack.enabled = false;
                break;
            case 4:
                bounceAttack.enabled = false;
                lungeAttack.enabled = true;
                lungeAttack.jumpCooldown = lungeCooldownPhase4;
                BossBounceAttack.bouncyPlatformStuns = false;
                projectileAttack.enabled = true;
                projectileAttack.shootCooldown = shootCooldownPhase4;
                projectileAttack.burstEnabled = false;
                beamAttack.enabled = true;
                break;
        }
        
        stunned = false;
        PhaseChange.Invoke(newPhase);
        phase = newPhase;
        Analytics.CustomEvent("CurrentPhase");
    }

    public void ResetBossFight()
    {
        health.Heal(health.maxHealth);
        transform.position = startPosition;
        SwitchPhase(1);
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("Startscreen");
    }
}
