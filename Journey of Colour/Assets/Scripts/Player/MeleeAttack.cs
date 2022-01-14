using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] internal float attackOffset = 1, attackRange = 1;
    [SerializeField] LayerMask opponentLayer;
    [SerializeField] Vector3 centerOffset = Vector3.zero;
    internal Vector3 attackBox;



    // Start is called before the first frame update
    void Start()
    {
        //de attackrange
        attackBox = new Vector3(attackRange / 2, attackRange / 4, attackRange / 2);
    }


    Vector3 BoxCenter
    {
        get { return transform.position + (transform.forward * attackOffset) + centerOffset; }
    }

    public virtual void Attack()
    {
        //maakt een array van alle colliders binnen de attackRange en als deze een health component hebben word er health afgehaald
        Collider[] overlaps;
        overlaps = Physics.OverlapBox(BoxCenter, attackBox, transform.rotation, opponentLayer);

        foreach (Collider opponent in overlaps)
        {
            if (opponent.GetComponent<EnemyHealth>() != null) opponent.GetComponent<EnemyHealth>().Damage(damage);
            else if (opponent.GetComponent<BossHealth>() != null) opponent.GetComponent<BossHealth>().Damage(damage);
            else if (opponent.GetComponent<PlayerHealth>() != null) opponent.GetComponent<PlayerHealth>().Damage(damage);
        }
    }
}
