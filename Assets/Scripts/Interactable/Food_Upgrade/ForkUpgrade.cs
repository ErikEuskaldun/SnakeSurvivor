using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkUpgrade : Upgrade, ILevelUp
{
    [SerializeField] ForkScriptable scriptable;
    [SerializeField] GameObject meatballPrefab;

    private void Awake()
    {
        UpdateInfo(scriptable, scriptable.evolution == null ? true : false);
    }

    private void Start()
    {
        time = scriptable.meatball.spawnTime-0.1f;
    }

    float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > scriptable.meatball.spawnTime)
        {
            time = 0;
            InstantiateMeatball();
        }
    }

    private void InstantiateMeatball()
    {
        Meatball newMeatball = Instantiate(meatballPrefab, Vector3.up * 100, Quaternion.identity, this.transform).GetComponent<Meatball>();

        newMeatball.Instantiate(scriptable.meatball);
        newMeatball.SetRandomPosition();
    }

    public void LevelUp()
    {
        if (scriptable.evolution != null)
        {
            scriptable = scriptable.evolution;
            UpdateInfo(scriptable, scriptable.evolution == null ? true : false);
            Debug.Log(scriptable.upgradeName + " Lvl Up " + (actualLevel - 1) + " -> " + actualLevel);
        }
    }
}
