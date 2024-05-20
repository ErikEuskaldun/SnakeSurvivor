using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] public List<Upgrade> foods = new List<Upgrade>();

    public List<GameObject> upgradePrefab = new List<GameObject>();

    private void Start()
    {
        //TODO: Instantiate apple for testing
    }

    public void NewUpgrade()
    {

    }

    public void LevelUpUpgrade()
    {

    }
}
