using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UpgradeScriptable upgradeInfoScriptable;
    [SerializeField] protected bool isMaxLevel = false;

    public bool IsMaxLevel { get => isMaxLevel; }
    [SerializeField] public string UpgradeName { get => upgradeInfoScriptable.upgradeName; }
    public int Level { get => upgradeInfoScriptable.level; }

    protected float time = 0f;

    protected void UpdateInfo(UpgradeScriptable scriptable, bool isMaxLevel)
    {
        upgradeInfoScriptable = scriptable;
        this.isMaxLevel = isMaxLevel;
    }

    protected virtual Food InstantiateFoodInRandomPosition(GameObject instance, FoodScriptable foodValues)
    {
        Food newFood = InstantiateFood(instance, foodValues);
        newFood.SetRandomPosition();
        return newFood;
    }

    protected virtual Food InstantiateFood(GameObject instance, FoodScriptable foodValues)
    {
        Food newFood = Instantiate(instance, Vector3.up * 100, Quaternion.identity, this.transform).GetComponent<Food>();
        newFood.Instantiate(foodValues);
        return newFood;
    }

    public virtual string LevelUpString(string name, int level)
    {
        return name + " Lvl Up " + (level - 1) + " -> " + level;
    }

    public void StartTimer(float foodTime)
    {
        time = foodTime - 0.1f;
    }

    public virtual void UpgradeLoop()
    {
        time += Time.deltaTime;
    }

    private void Update()
    {
        UpgradeLoop();
    }
}
