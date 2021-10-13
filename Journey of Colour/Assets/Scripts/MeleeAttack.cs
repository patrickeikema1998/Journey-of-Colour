using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float attackOffset = 1,
                           attackRange = 1;
    [SerializeField] LayerMask opponentLayer;
    Vector3 attackBox;

    // Start is called before the first frame update
    void Start()
    {
        attackBox = new Vector3(attackRange / 2, attackRange / 4, attackRange / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Attack();
    }

    public void Attack()
    {
        Collider[] overlaps = Physics.OverlapBox(transform.position + (transform.forward * attackOffset), attackBox, transform.rotation, opponentLayer);
        foreach (Collider opponent in overlaps)
        {
            if (opponent.GetComponent<Health>() != null) opponent.GetComponent<Health>().Damage(damage);
        }
    }
}
