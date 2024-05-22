using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleUpgrade : Upgrade, ILevelUp
{
    [SerializeField] AppleScriptable scriptable;
    [SerializeField] GameObject applePrefab, rottenApplePrefab;

    private void Awake()
    {
        UpdateInfo(scriptable, scriptable.evolution == null ? true : false);
    }

    private void Start()
    {
        time = scriptable.apple.spawnTime-0.1f;
    }

    float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > scriptable.apple.spawnTime)
        {
            time = 0;
            InstantiateApple();
        }
    }

    private void InstantiateApple()
    {
        Apple newApple = Instantiate(applePrefab, Vector3.up * 100, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.OnDespawnEvent.AddListener(InstantiateRottenApple);

        newApple.Instantiate(scriptable.apple);
        newApple.SetRandomPosition();
    }

    private void InstantiateRottenApple(Vector3 position)
    {
        Apple newRottenApple = Instantiate(rottenApplePrefab, Vector3.up * 100, Quaternion.identity, this.transform).GetComponent<Apple>();

        newRottenApple.Instantiate(scriptable.badApple);
        newRottenApple.transform.position = position;
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
