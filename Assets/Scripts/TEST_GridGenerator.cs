using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_GridGenerator : MonoBehaviour
{
    public int startY, endY, startX, endX;
    public GameObject floor, brick;
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
                if(y == startY || y == endY || x == startX || x == endX)
                    Instantiate(brick, new Vector3(x, y), Quaternion.identity, this.transform);
                else
                {
                    SpriteRenderer s = Instantiate(floor, new Vector3(x, y), Quaternion.identity, this.transform).GetComponent<SpriteRenderer>();
                    s.color = (x + y) % 2 == 0 ? colorA : colorB;
                }
            }
        }
    }
}
