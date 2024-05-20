using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Upgrade/New Apple", order = 1)]
public class AppleScriptable : UpgradeScriptable
{
    [Header("Apple Variables")]
    public AppleScriptable evolution;

    [Header("Apple Stats")]
    public FoodScriptable apple;
    public FoodScriptable badApple;
}
