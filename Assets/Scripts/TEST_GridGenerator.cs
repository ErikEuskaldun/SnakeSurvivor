using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_GridGenerator : MonoBehaviour
{
    public int startY, endY, startX, endX;
    public GameObject prefab;
    public Color colorA, colorB;
    void Start()
    {
        GenerateGrid(startY, endY, startX, endX);
    }

    public void GenerateGrid(int startY, int endY, int startX, int endX)
    {
        for (int y = startY; y < endY+1; y++)
        {
            for (int x = startX; x < endX+1; x++)
            {
                SpriteRenderer s = Instantiate(prefab, new Vector3(x,y),Quaternion.identity).GetComponent<SpriteRenderer>();
                s.color = (x+y) % 2 == 0 ? colorA : colorB;
            }
        }
    }
}
