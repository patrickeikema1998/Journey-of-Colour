using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{

    Vector2 randomSoundPitch = new Vector2(1, 1.31f);
    [SerializeField] AudioSource damageSound;

    // Start is called before the first frame update
    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public override void Damage(int damageAmount)
    {
        //sounds
        if ((health - damageAmount) >= 0) damageSound.Play();
        base.Damage(damageAmount);
        GetComponentInChildren<EnemyAnimations>().GetHit();
    }
}
