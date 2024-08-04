using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Info", menuName = "ScriptableObjects/Upgrade/New Upgrade Info", order = 0)]
public class UpgradeScriptable : ScriptableObject
{
    [Header("Upgrade Info Variables")]
    public string upgradeName;
    public int level;
    [TextArea(2,5)]
    public string description;
    public Sprite image;
    public float spawnTime = 10;
    public FoodRate[] food;
    public UpgradeScriptable evolution;
}

[System.Serializable]
public class FoodRate
{
    public GameObject food;
    public int rate = 100;
}

