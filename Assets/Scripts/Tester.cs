using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] List<GameObject> perfabs;
    [SerializeField] List<FoodScriptable> foods;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            SpawnSomething();
    }
    public void SpawnSomething()
    {
        int r = Random.Range(0, perfabs.Count);
        Food f = Instantiate(perfabs[r]).GetComponent<Food>();
        //f.Instantiate(foods[r]);
        f.SetRandomPosition();
    }
}
