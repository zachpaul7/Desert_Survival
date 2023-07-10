using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Bson;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersistentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI upgradeName;

    [SerializeField] DataContainer dataContainer;

    private void Start()
    {
        UpdateElement();
    }
    public void Upgarde()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        if(playerUpgrades.level >= playerUpgrades.max_level) { return; }
       
        if(dataContainer.coins >= playerUpgrades.costToUpgrade)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }
    void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        upgradeName.text = upgrade.ToString();
        level.text = playerUpgrades.level.ToString();
        price.text = playerUpgrades.costToUpgrade.ToString();
    }
}
