using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkUpgrade : Upgrade, IUpgrade
{
    [SerializeField] ForkScriptable scriptable;
    [SerializeField] GameObject meatballPrefab;

    private void Start()
    {
        StartTimer(scriptable.meatball.spawnTime);
    }

    public override void UpgradeLoop()
    {
        base.UpgradeLoop();
        if (time > scriptable.meatball.spawnTime)
        {
            time = 0;
            InstantiateFoodInRandomPosition(meatballPrefab, scriptable.meatball); //Instantiate meatball
        }
    }

    public void LevelUp()
    {
        if (scriptable.evolution != null)
        {
            scriptable = scriptable.evolution;
            UpdateInfo();
            Debug.Log(LevelUpString(scriptable.upgradeName, scriptable.level));
        }
    }

    public void UpdateInfo()
    {
        UpdateInfo(scriptable, scriptable.evolution == null ? true : false);
    }

    public UpgradeScriptable NextLevelScriptable()
    {
        if (!IsMaxLevel)
            return scriptable.evolution;
        return null;
    }
}
