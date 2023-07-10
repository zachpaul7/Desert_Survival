using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int dmg;
    public float timeToAttack;
    public int numberOfAttacks;
    public int OrbitSpeed;

    public WeaponStats(int dmg, float timeToAttack, int numberOfAttacks, int OrbitSpeed)
    {
        this.dmg = dmg;
        this.timeToAttack = timeToAttack;
        this.numberOfAttacks = numberOfAttacks;
        this.OrbitSpeed = OrbitSpeed;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.dmg += weaponUpgradeStats.dmg;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        this.OrbitSpeed += weaponUpgradeStats.OrbitSpeed;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}