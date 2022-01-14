using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{

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
        base.Damage(damageAmount);
        GetComponentInChildren<EnemyAnimations>().GetHit();
    }
}
