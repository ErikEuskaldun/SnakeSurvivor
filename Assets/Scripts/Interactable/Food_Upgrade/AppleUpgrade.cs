using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleUpgrade : Upgrade, IUpgrade
{
    [SerializeField] AppleScriptable scriptable;
    [SerializeField] GameObject applePrefab, rottenApplePrefab;

    private void Start()
    {
        StartTimer(scriptable.apple.spawnTime);
    }

    public override void UpgradeLoop()
    {
        base.UpgradeLoop();
        if (time > scriptable.apple.spawnTime)
        {
            time = 0;
            InstantiateApple();
        }
    }

    private void InstantiateApple()
    {
        Food newApple = base.InstantiateFoodInRandomPosition(applePrefab, scriptable.apple);
        newApple.OnDespawnEvent.AddListener(InstantiateRottenApple);
    }

    private void InstantiateRottenApple(Vector3 position)
    {
        Food newRottenApple = InstantiateFood(rottenApplePrefab, scriptable.badApple);
        newRottenApple.transform.position = position;
    }

    public void LevelUp()
    {
        if (scriptable.evolution != null)
        {
            scriptable = scriptable.evolution;
            UpdateInfo();
            Debug.Log(LevelUpString(scriptable.name, scriptable.level));
        }
    }

    public void UpdateInfo()
    {
        UpdateInfo(scriptable, scriptable.evolution == null ? true : false);
    }

    public UpgradeScriptable NextLevelScriptable()
    {
        if(!IsMaxLevel)
            return scriptable.evolution;
        return null;
    }
}
