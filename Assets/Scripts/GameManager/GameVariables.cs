using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    public static float pointMultiplier = 1;
    public static float spawnrateMultiplier = 1;
    public static float foodLifeTimeMultiplier = 1;

    public static void SetDefaultVariables()
    {
        pointMultiplier = 1;
        spawnrateMultiplier = 1;
        foodLifeTimeMultiplier = 1;
    }
}
