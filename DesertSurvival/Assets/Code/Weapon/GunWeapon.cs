using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponBase
{
    [SerializeField] GameObject bulletPrefab;
    Scanner scanner;

    private void Start()
    {
        scanner = FindObjectOfType<Scanner>();
    }

    public override void Attack()
    {
        
        if (!scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = scanner.nearestTarget.position;
        Vector3 direction = (targetPos - transform.position).normalized;
     
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.SetParent(null);

        bullet.GetComponent<ThrowingKnifeProjectile>().SetDirection(direction.x, direction.y);
        bullet.GetComponent<ThrowingKnifeProjectile>().dmg = GetDamage();
    }
}
