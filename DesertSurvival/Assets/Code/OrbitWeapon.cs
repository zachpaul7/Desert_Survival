using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class OrbitWeapon : WeaponBase
{
    [SerializeField] GameObject OrbitPrefab;
    GameObject[] Orbits;

    public float circleR;
    float objSpeed;
    float deg;
    int objSize;

    void UpdateOrbits()
    {
        objSpeed = weaponStats.OrbitSpeed;
        objSize = weaponStats.numberOfAttacks;

        if (Orbits == null || Orbits.Length != objSize)
        {
            if (Orbits != null)
            {
                foreach (var orbit in Orbits)
                {
                    Destroy(orbit);
                }
            }

            Orbits = new GameObject[objSize];

            for (int i = 0; i < objSize; i++)
            {
                Orbits[i] = Instantiate(OrbitPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void MoveOrbits()
    {
        deg += Time.deltaTime * objSpeed;

        if (deg < 360)
        {
            for (int i = 0; i < objSize; i++)
            {
                var rad = Mathf.Deg2Rad * (deg + (i * (360 / objSize)));
                var x = circleR * Mathf.Sin(rad);
                var y = circleR * Mathf.Cos(rad);

                Orbits[i].transform.position = transform.position + new Vector3(x, y, 0);
                Orbits[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / objSize))) * -1);
            }
        }
        else
        {
            deg = 0;
        }
    }

    void ApplyDamage()
    {

        if (Time.frameCount % 30 == 0)
        { 
            for (int i = 0; i < objSize; i++)
            {
                Collider2D[] hit = Physics2D.OverlapCircleAll(Orbits[i].transform.position, 1f);
                base.ApplyDamage(hit);

            }
        }
    }

    public override void Attack()
    {
        UpdateOrbits();
        MoveOrbits();
        ApplyDamage();
    }
}