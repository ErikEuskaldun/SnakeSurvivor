using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] GameObject pointFXPrefab;

    public void InstantiatePointFX(Vector2 position, int points)
    {
        PointFX pointFX = Instantiate(pointFXPrefab, position+Vector2.up, Quaternion.identity, this.transform).GetComponent<PointFX>();
        pointFX.InstantiatePointsFX(points);
    }

    
}
