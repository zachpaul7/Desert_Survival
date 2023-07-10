using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHp = 1000;

    public int armor = 0;

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;

    public float damageBonus;
    public float speedBonus;

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    [HideInInspector] public bool isDead;

    [SerializeField] DataContainer dataContainer;

    void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    void Start()
    {
        ApplyPersistantUpgrades();
        hpBar.SetState(currentHp, maxHp);
    }

    private void ApplyPersistantUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);

        maxHp += maxHp / 10 * hpUpgradeLevel;
        currentHp = maxHp;

        int damageUpgradeLevel  = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);

        damageBonus = 1f + 0.1f * damageUpgradeLevel;

        int speedUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Speed);

        speedBonus = 1f + 0.05f * speedUpgradeLevel;
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if(hpRegenerationTimer > 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
        }
    }

    public void TakeDamage(int dmg)
    {
        if(isDead == true) { return; }

        ApplyArmor(ref dmg);

        currentHp -= dmg;
        if(currentHp < 1)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }
        hpBar.SetState(currentHp, maxHp);
    }

    private void ApplyArmor(ref int dmg)
    {
        dmg -= armor;
        if (dmg < 0) { dmg = 0; }
    }

    public void Heal(int amount)
    {
        if (currentHp <= 0) { return; }

        currentHp += amount;
        
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        hpBar.SetState(currentHp, maxHp);
    }
}
