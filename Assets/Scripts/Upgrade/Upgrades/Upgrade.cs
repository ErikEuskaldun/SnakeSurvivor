using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UpgradeScriptable scriptable;
    public bool IsMaxLevel { get => scriptable.evolution == null ? true : false; }
    [SerializeField] public string UpgradeName { get => scriptable.upgradeName; }
    public int Level { get => scriptable.level; }

    protected float time = 0f;

    private void Start()
    {
        StartTimer(scriptable.spawnTime);
    }

    protected virtual Food InstantiateFoodInRandomPosition(GameObject instance)
    {
        Food newFood = InstantiateFood(instance);
        newFood.SetRandomPosition();
        return newFood;
    }

    protected virtual Food InstantiateFood(GameObject instance)
    {
        Food newFood = Instantiate(instance, Vector3.up * 100, Quaternion.identity, this.transform).GetComponent<Food>();
        newFood.Instantiate();
        return newFood;
    }

    public virtual void LevelUp()
    {
        Debug.Log("MainLevelUpo");
        if (scriptable.evolution != null)
            scriptable = scriptable.evolution;
        Debug.Log(LevelUpString());
    }

    public virtual string LevelUpString()
    {
        return scriptable.name + " Lvl Up " + (scriptable.level - 1) + " -> " + scriptable.level;
    }

    public void StartTimer(float foodTime)
    {
        time = foodTime - 0.1f;
    }

    public virtual void UpgradeLoop()
    {
        time += Time.deltaTime;
        if(scriptable.spawnTime < time)
        {
            time = 0;
            SpawnRandomFood();
        }
    }

    private void SpawnRandomFood()
    {
        int percent = Random.Range(1, 101);

        int percentChecker = 0;
        foreach (FoodRate fr in scriptable.food)
        {
            percentChecker += fr.rate;
            if (percent <= percentChecker)
                InstantiateFoodInRandomPosition(fr.food);

        }
    }

    private void Update()
    {
        UpgradeLoop();
    }
}
