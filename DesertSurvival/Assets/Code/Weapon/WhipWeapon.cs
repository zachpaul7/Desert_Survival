using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] Vector2 attackSize = new Vector2(6f, 2f);

    [SerializeField] GameObject leftWhipObj;
    [SerializeField] GameObject rightWhipObj;

    public override void Attack()
    {
        UpdateVectorOfAttack();

        if (vectorOfAttack.x > 0)
        {
            rightWhipObj.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObj.transform.position, attackSize, 0f);
            ApplyDamage(colliders);
        }
        else
        {
            leftWhipObj.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObj.transform.position, attackSize, 0f);
            ApplyDamage(colliders);
        }

        if (weaponStats.numberOfAttacks > 1)
        {
            if (vectorOfAttack.x < 0)
            {
                rightWhipObj.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObj.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                leftWhipObj.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObj.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
        }
    }
}
