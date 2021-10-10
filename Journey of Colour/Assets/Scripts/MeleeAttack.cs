using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject meleeWeapon;
    [SerializeField] float weaponOffset = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Instantiate(meleeWeapon, transform.position + transform.forward * weaponOffset, transform.rotation, transform);
    }
}
