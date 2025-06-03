using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOrangeColor : Upgrade
{
    private void Start()
    {
        StartTimer(scriptable.spawnTime);
        FoodLifeEffect();
    }

    public override void LevelUp()
    {
        if (scriptable.evolution != null)
            scriptable = scriptable.evolution;
        Debug.Log(LevelUpString());
        FoodLifeEffect();
    }

    private void FoodLifeEffect()
    {
        int upgradelevel = scriptable.level;
        switch (upgradelevel)
        {
            case 1: BoostFoodLife(0.1f); break;
            case 2: BoostFoodLife(0.15f); break;
            case 3: BoostFoodLife(0.2f); break;
            case 4: BoostFoodLife(0.4f); break;
        }
    }

    private void BoostFoodLife(float value)
    {
        GameVariables.foodLifeTimeMultiplier += value;
    }
}
