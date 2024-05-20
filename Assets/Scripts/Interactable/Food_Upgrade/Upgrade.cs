using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int actualLevel = 1;

    public void LevelUp()
    {
        actualLevel += 1;
    }
}
