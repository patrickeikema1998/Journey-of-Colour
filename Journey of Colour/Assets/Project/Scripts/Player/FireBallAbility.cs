using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour
{
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] int fireBallSpeed;
    [SerializeField] float fireBallOffsetX;
    [SerializeField] float fireBallOffsetY;
    [SerializeField] AudioSource fireSound;

    Vector3 leftRotation, rightRotation;
    GameObject player;
    PlayerHealth playerHealth;
    PlayerAnimations anim;
    GameObject fireBall;
    FireBall fireBallScript;
    CustomTimer shootCooldownTimer;

    public int damage = 5;
    public float shootCooldownInSeconds;
    float shootAnimationTime = 0.4f;

    private void Start()
    {
        // We set the rotation of the player, so we know what direction the FireBall needs to go.
        rightRotation = new Vector3(0, 90, 0);
        leftRotation = new Vector3(0, 270, 0);
        player = transform.parent.gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<PlayerAnimations>();
        fireBallScript = fireBallPrefab.GetComponent<FireBall>();

        // We need a cooldown on the FireBall so that you can't spam it.
        shootCooldownTimer = new CustomTimer(shootCooldownInSeconds);
        shootCooldownTimer.start = true;
        shootCooldownTimer.finish = true;
    }

    void Update()
    {
        shootCooldownTimer.Update();

        if (Input.GetKeyDown(GameManager.GM.fireBallAbility) && shootCooldownTimer.finish && !playerHealth.dead)
        {
            anim.RangeAttack();
            fireSound.Play();

            // We use Invoke because we need the animation to play first and after that do the Shoot function.
            Invoke("Shoot", shootAnimationTime);

            shootCooldownTimer.Reset();
        }
    }

    // This method will check if the player is looking to the left or the rigt.
    void Shoot()
    {
        // If the player is looking right, there will be a fireball instantiation.
        // The fireballprefab will be used. spawns on the position of the player + offset (x and y).
        // Then the speed on the fireballscript of that fireball will be set to the fireballspeed in this class.
        if (player.transform.rotation.eulerAngles == rightRotation)
        {
            fireBall = Instantiate(fireBallPrefab, new Vector3(player.transform.position.x + fireBallOffsetX, player.transform.position.y + fireBallOffsetY, player.transform.position.z), Quaternion.identity);
            fireBallScript = fireBall.GetComponent<FireBall>();
            fireBallScript.speed = fireBallSpeed;
        }
        // If the player is looking left, there will be a fireball instantiation.
        // The fireballprefab will be used. spawns on the position of the player - offset x and + offset y.
        // Then the speed on the fireballscript of that fireball will be set to the -fireballspeed (- because it needs to go left) in this class.
        else if (player.transform.rotation.eulerAngles == leftRotation)
        {
            fireBall = Instantiate(fireBallPrefab, new Vector3(player.transform.position.x - fireBallOffsetX, player.transform.position.y + fireBallOffsetY, player.transform.position.z), Quaternion.identity);
            fireBallScript = fireBall.GetComponent<FireBall>();
            fireBallScript.speed = -fireBallSpeed;
        }
        // The fireball will also do damage.
        fireBallScript.damage = damage;
    }
}
