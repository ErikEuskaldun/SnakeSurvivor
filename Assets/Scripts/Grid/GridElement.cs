using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    public Grid grid;
    public Vector2Int position;
    public EElementType type;

    void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        grid.gridElements.Add(this);
    }
}
