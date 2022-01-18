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
        //returns the center of the attack box
        get { return transform.position + (transform.forward * attackOffset) + centerOffset; }
    }

    public virtual void Attack()
    {
        //maakt een array van alle colliders binnen de attackRange en als deze een health component hebben word er health afgehaald
        Collider[] overlaps;
        overlaps = Physics.OverlapBox(BoxCenter, attackBox, transform.rotation, opponentLayer);

        foreach (Collider opponent in overlaps)
        {
            //checks wether the opponet has a health component and damages it
            if (opponent.GetComponent<Health>() != null) opponent.GetComponent<Health>().Damage(damage);
        }
    }
}
