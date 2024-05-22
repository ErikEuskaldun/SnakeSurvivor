using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Upgrade/New Fork", order = 1)]
public class ForkScriptable : UpgradeScriptable
{
    [Header("Fork Variables")]
    public ForkScriptable evolution;

    [Header("Fork Food")]
    public FoodScriptable meatball;
}
