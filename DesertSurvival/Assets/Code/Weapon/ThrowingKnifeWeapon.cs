using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{
    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();

        for(int i = 0; i<weaponStats.numberOfAttacks; i++)
        {
            GameObject thrownKnife = Instantiate(knifePrefab);

            Vector3 newKinfePosition = transform.position;


            if(weaponStats.numberOfAttacks > 1)
            {
                newKinfePosition.y -= (spread * weaponStats.numberOfAttacks) / 2;
                newKinfePosition.y += i * spread;
            }

            thrownKnife.transform.position = newKinfePosition;
            thrownKnife.transform.rotation = Quaternion.FromToRotation(Vector3.up, vectorOfAttack);

            ThrowingKnifeProjectile throwingDaggerProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
            throwingDaggerProjectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
            throwingDaggerProjectile.dmg = GetDamage();
        }
    }
}
