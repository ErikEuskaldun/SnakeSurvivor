using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePotassiumK : Upgrade
{
    private void Start()
    {
        StartTimer(scriptable.spawnTime);
        SpawnRateEffect();
    }

    public override void LevelUp()
    {
        if (scriptable.evolution != null)
            scriptable = scriptable.evolution;
        Debug.Log(LevelUpString());
        SpawnRateEffect();
    }

    private void SpawnRateEffect()
    {
        int upgradelevel = scriptable.level;
        switch (upgradelevel)
        {
            case 1: BoostSpawnRate(0.1f); break;
            case 2: BoostSpawnRate(0.15f); break;
            case 3: BoostSpawnRate(0.2f); break;
            case 4: BoostSpawnRate(0.4f); break;
        }
    }

    private void BoostSpawnRate(float value)
    {
        GameVariables.spawnrateMultiplier += value;
    }
}
