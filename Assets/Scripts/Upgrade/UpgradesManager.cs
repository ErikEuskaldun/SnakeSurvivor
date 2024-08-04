using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> upgrades = new List<GameObject>();

    public List<GameObject> upgradePrefabs = new List<GameObject>();
    private Dictionary<string, GameObject> upgradeDictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        GenerateDicionary();
        int random = Random.Range(0, 0);

        string randomUpgradeName = upgradePrefabs[random].GetComponent<Upgrade>().UpgradeName;
        NewUpgrade(randomUpgradeName);
    }

    public void UpgradeSelector()
    {
        List<UpgradeScriptable> upgrades = new List<UpgradeScriptable>();

        UpgradeScriptable uScriptable = default;
        do
        {
            uScriptable = GetRandomUpgrade(upgrades);
            if (uScriptable != null)
                upgrades.Add(uScriptable);
        } while (uScriptable!=null && upgrades.Count!=3);

        if (upgrades.Count != 0)
            FindObjectOfType<UpgradeSelector>().GenerateUpgradeSelection(upgrades);
        else
            Debug.Log("No Upgrades Available");
    }

    private UpgradeScriptable GetRandomUpgrade(List<UpgradeScriptable> repeatedList)
    {
        bool newUpgrade = false;
        bool lvlUpUpgrade = false;

        List<UpgradeScriptable> posibleEvolutions = new List<UpgradeScriptable>();
        List<UpgradeScriptable> posibleNewUpgrade = new List<UpgradeScriptable>();

        if (upgrades.Count != 4 && upgradePrefabs.Count != 0)
        {
            foreach (GameObject upgradeGO in upgradePrefabs)
            {
                Upgrade upgrade = upgradeGO.GetComponent<Upgrade>();
                if (!repeatedList.Contains(upgrade.scriptable))
                    posibleNewUpgrade.Add(upgrade.scriptable);
            }
            if(posibleNewUpgrade.Count!=0)
                newUpgrade = true;
        }
            

        foreach (GameObject upgradeGO in upgrades)
        {
            Upgrade upgrade = upgradeGO.GetComponent<Upgrade>();
            //IUpgrade iUpgrade = upgrade.GetComponent(typeof(IUpgrade)) as IUpgrade;

            if (!upgrade.IsMaxLevel && !repeatedList.Contains(upgrade.scriptable.evolution))
                posibleEvolutions.Add(upgrade.scriptable.evolution);
        }
        
        if (posibleEvolutions.Count != 0)
            lvlUpUpgrade = true;

        int r;
        if(lvlUpUpgrade && newUpgrade)
        {
            r = Random.Range(0, 2);
            if (r == 0)
                newUpgrade = false;
            else
                lvlUpUpgrade = false;
        }

        if (newUpgrade)
        {
            int random = Random.Range(0, posibleNewUpgrade.Count);
            UpgradeScriptable randomUpgrade = posibleNewUpgrade[random];
            return randomUpgrade;
        }
        else if(lvlUpUpgrade)
        {
            int random = Random.Range(0, posibleEvolutions.Count);
            UpgradeScriptable randomUpgrade = posibleEvolutions[random];
            return randomUpgrade;
        }

        return null;
    }

    private void GenerateDicionary()
    {
        foreach (GameObject upgradePrefab in upgradePrefabs)
        {
            Upgrade upgrade = upgradePrefab.GetComponent<Upgrade>();
            string uName = upgrade.UpgradeName;
            upgradeDictionary.Add(uName, upgradePrefab);
        }
    }

    public void LevelUpUpgrade(string upgradeName)
    {
        GameObject upgrade = default;
        foreach (GameObject u in upgrades)
        {
            string uName = u.GetComponent<Upgrade>().scriptable.upgradeName;
            if (uName == upgradeName)
                upgrade = u;
        }
        upgrade.GetComponent<Upgrade>().LevelUp();

        UpdateInfo(upgrades.IndexOf(upgrade));
    }

    public void NewUpgrade(string upgradeName)
    {
        GameObject upgrade = upgradeDictionary[upgradeName];
        upgradePrefabs.Remove(upgrade);

        GameObject i = Instantiate(upgrade, this.transform);
        Debug.Log("New Upgrade: " + i.GetComponent<Upgrade>().UpgradeName);

        upgrades.Add(i);

        UpdateInfo(upgrades.IndexOf(i));
    }

    private void UpdateInfo(int upgradePosition)
    {
        Upgrade upgrade = upgrades[upgradePosition].GetComponent<Upgrade>();
        FindObjectOfType<UpgradeMenu>().ChangeUpgradeInfo(upgrade, upgradePosition);
    }
}
