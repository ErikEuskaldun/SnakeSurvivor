using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int actualLevel = 1;
    [SerializeField] protected string upgradeName;
    [SerializeField] protected bool isMaxLevel = false;

    public string UpgradeName { get => upgradeName; }
    public bool IsMaxLevel { get => isMaxLevel; }

    protected void UpdateInfo(UpgradeScriptable scriptable, bool isMaxLevel)
    {
        actualLevel = scriptable.level;
        upgradeName = scriptable.upgradeName;
        this.isMaxLevel = isMaxLevel;
    }
}
