using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAppleTree : Upgrade
{
    private void Start()
    {
        StartTimer(scriptable.spawnTime);
        XpBoostEffect();
    }

    public override void LevelUp()
    {
        if (scriptable.evolution != null)
            scriptable = scriptable.evolution;
        Debug.Log(LevelUpString());
        XpBoostEffect();
    }

    private void XpBoostEffect()
    {
        int upgradelevel = scriptable.level;
        switch (upgradelevel)
        {
            case 1: case 2: BoostXP(0.15f); break;
            case 3: BoostXP(0.2f); break;
            case 4: BoostXP(0.5f); break;
        }
    }

    private void BoostXP(float value)
    {
        FindObjectOfType<GameVariables>().pointMultiplier += value;
    }
}
