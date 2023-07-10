using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;
    PassiveItem passiveItem;

    [SerializeField] List<UpgradeData> upgradesAvailableOnStart;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItem = GetComponent<PassiveItem>();
    }

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrade(upgradesAvailableOnStart);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }
    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        if(acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>();  }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;

            case UpgradeType.ItemUpgrade:
                passiveItem.UpgradeItem(upgradeData);
                break;

            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;

            case UpgradeType.ItemGet:
                passiveItem.Equip(upgradeData.item);
                AddUpgradesIntoTheListOfAvailableUpgrade(upgradeData.item.upgrades);
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    private void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData> (); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));

        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public void ShuffleUpgrades()
    {
        for(int i = upgrades.Count -1; i > 0; i--)
        {
            int x = Random.Range(0, i + 1);
            UpgradeData shuffleElement = upgrades[i];
            upgrades[i] = upgrades[x];
            upgrades[x] = shuffleElement;
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades();

        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[i]);
        }

        return upgradeList;
    }

    public void AddUpgradesIntoTheListOfAvailableUpgrade(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null) { return; }   
        this.upgrades.AddRange(upgradesToAdd);
    }
}
