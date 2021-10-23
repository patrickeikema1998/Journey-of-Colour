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
        //de attackrange
        attackBox = new Vector3(attackRange / 2, attackRange / 4, attackRange / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position + transform.forward, transform.position + (transform.forward * attackOffset));
    }

    public void Attack()
    {
        //maakt een array van alle colliders binnen de attackRange en als deze een health component hebben word er health afgehaald
        Collider[] overlaps;
        if (tag.Equals("Player"))
        {
            int playerDirection = 0;
            if (Input.GetAxis("Horizontal") < 0) playerDirection = 1;
            if (Input.GetAxis("Horizontal") > 0) playerDirection = -1;
            overlaps = Physics.OverlapBox(transform.position + (transform.right * attackOffset * playerDirection), attackBox, transform.rotation, opponentLayer);
        } else
        {
            overlaps = Physics.OverlapBox(transform.position + (transform.forward * attackOffset), attackBox, transform.rotation, opponentLayer);
        }

        foreach (Collider opponent in overlaps)
        {
            if (opponent.GetComponent<Health>() != null) opponent.GetComponent<Health>().Damage(damage);
        }
    }
}
