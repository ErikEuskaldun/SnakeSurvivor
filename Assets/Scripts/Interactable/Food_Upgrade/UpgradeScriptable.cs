using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Info", menuName = "ScriptableObjects/Upgrade/New Upgrade Info", order = 0)]
public class UpgradeScriptable : ScriptableObject
{
    [Header("Upgrade Info Variables")]
    public string upgradeName;
    public int level;
}
