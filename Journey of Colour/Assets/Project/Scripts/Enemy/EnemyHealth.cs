using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    [SerializeField] AudioSource damageSound;

    private void Start()
    {
        // Get the slider (healthbar) from the Enemy gameobject.
        slider = GetComponentInChildren<Slider>();
    }

    public override void Damage(int damageAmount)
    {
        // Is there is still health left, sound will be played.
        if ((health - damageAmount) > 0) damageSound.Play();
        base.Damage(damageAmount);
        GetComponentInChildren<EnemyAnimations>().GetHit();
    }
}
