using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food/New Generic Food", order = 0)]
public class FoodScriptable : ScriptableObject
{
    [Header("Food Variables")]
    public string foodName;
    public int points;
    public int tailLength;
    public float despawnTime;
}
