using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPersistentUpgrades
{
    HP,
    Damage,
    Speed
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPersistentUpgrades persistentUpgrades;
    public int level = 0;
    public int max_level = 10;
    public int costToUpgrade = 100;

}

[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public List<bool> stageCompletion;

    public List<PlayerUpgrades> upgrades;

    public void StageComplete(int i)
    {
        stageCompletion[i] = true;
    }

    public int GetUpgradeLevel(PlayerPersistentUpgrades persistentUpgrades)
    {
        return upgrades[(int)persistentUpgrades].level;
    }
}
