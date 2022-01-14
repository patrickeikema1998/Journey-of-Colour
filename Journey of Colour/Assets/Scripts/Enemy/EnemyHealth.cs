using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{

    Vector2 randomSoundPitch = new Vector2(1, 1.31f);

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        DeadCheck();
    }

    public override void Damage(int damageAmount)
    {
        if((health - damageAmount) > 0) AudioManager.instance.PlayOrStop(GetComponent<EnemyType>().typeOfEnemy + "GetHit", true, randomSoundPitch);
        base.Damage(damageAmount);
        GetComponentInChildren<EnemyAnimations>().GetHit();
    }
}
