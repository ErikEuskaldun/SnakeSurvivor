using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "ScriptableObjects/Upgrade/New Generic Upgrade", order = 0)]
public class UpgradeScriptable : ScriptableObject
{
    [Header("Upgrade Variables")]
    public string id;
    public string upgradeName;
    public int level = 1;
}
