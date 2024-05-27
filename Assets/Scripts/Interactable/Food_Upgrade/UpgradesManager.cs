using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> upgrades = new List<GameObject>();

    public List<GameObject> upgradePrefabs = new List<GameObject>();
    private Dictionary<string, GameObject> upgradeDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        
    }

    private void Start()
    {
        GenerateDicionary();
        int random = Random.Range(0, 0);

        string randomUpgradeName = upgradePrefabs[random].GetComponent<Upgrade>().UpgradeName;
        NewUpgrade(randomUpgradeName);
    }

    public void TEST_RandomUpgrade()
    {
        bool newUpgrade = false;
        bool lvlUpUpgrade = false;

        List<GameObject> posibleEvolutions = new List<GameObject>();

        if (upgrades.Count != 4 && upgradePrefabs.Count != 0)
            newUpgrade = true;

        foreach (GameObject upgradeGO in upgrades)
        {
            Upgrade upgrade = upgradeGO.GetComponent<Upgrade>();
            if (!upgrade.IsMaxLevel)
                posibleEvolutions.Add(upgradeGO);
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
            int random = Random.Range(0, upgrades.Count);
            string randomUpgradeName = upgradePrefabs[random].GetComponent<Upgrade>().UpgradeName;
            NewUpgrade(randomUpgradeName);
        }
        else if(lvlUpUpgrade)
        {
            int random = Random.Range(0, posibleEvolutions.Count);
            GameObject randomUpgrade = posibleEvolutions[random];
            IUpgrade upgrade = randomUpgrade.GetComponent(typeof(IUpgrade)) as IUpgrade;
            upgrade.LevelUp();
        }

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

    public void NewUpgrade(string upgradeName)
    {
        GameObject upgrade = upgradeDictionary[upgradeName];
        upgradePrefabs.Remove(upgrade);

        GameObject i = Instantiate(upgrade, this.transform);
        Debug.Log("New Upgrade: " + i.GetComponent<Upgrade>().UpgradeName);

        upgrades.Add(i);
    }
}
