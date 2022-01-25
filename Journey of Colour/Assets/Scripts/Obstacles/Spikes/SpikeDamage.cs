using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    const string _PlayerTag = "Player";

    [SerializeField] float secondsBetweenDamage;
    [SerializeField] int damage;

    CustomTimer betweenDamageTimer;

    // Start is called before the first frame update
    void Start()
    {
        betweenDamageTimer = new CustomTimer(secondsBetweenDamage);
        betweenDamageTimer.start = true;
    }

    private void Update()
    {
        betweenDamageTimer.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        //Damages the player if colliding with the spikes. Only damages every couple of seconds to prevent instant death.
        if (other.gameObject.tag == _PlayerTag)
        {
            if (other.GetComponent<PlayerHealth>() != null && betweenDamageTimer.finish)
            {
                other.GetComponent<PlayerHealth>().Damage(damage);
                betweenDamageTimer.Reset();
            }
        }
    }
}
