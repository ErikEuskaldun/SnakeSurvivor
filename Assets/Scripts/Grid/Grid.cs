using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<GridElement> gridElements = new List<GridElement>();
    public int startY, endY, startX, endX;
    public GameObject floor, brick;
    public Color colorA, colorB;
    void Start()
    {
        GenerateGrid(startY, endY, startX, endX);
    }

    public void GenerateGrid(int startY, int endY, int startX, int endX)
    {
        for (int y = startY; y < endY + 1; y++)
        {
            for (int x = startX; x < endX + 1; x++)
            {
                if (y == startY || y == endY || x == startX || x == endX)
                {
                    GridElement instance = Instantiate(brick, new Vector3(x, y) * SnakeUtils.TILE_SIZE, Quaternion.identity, this.transform).GetComponent<GridElement>();
                    instance.position = new Vector2Int(x, y);
                }
                else
                {
                    SpriteRenderer s = Instantiate(floor, new Vector3(x, y) * SnakeUtils.TILE_SIZE, Quaternion.identity, this.transform).GetComponent<SpriteRenderer>();
                    s.color = (x + y) % 2 == 0 ? colorA : colorB;
                }
            }
        }

        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        string[,] layout = GetComponent<GridLayout>().ReadCSV();
        for (int x = 0; x < layout.GetLength(0); x++)
        {
            for (int y = 0; y < layout.GetLength(1); y++)
            {
                if (layout[x, y] == "1")
                {
                    Debug.Log((-10 + x) + "/" + (10 - y));
                    GridElement instance = Instantiate(brick, new Vector3(-10 + x, 10 - y) * SnakeUtils.TILE_SIZE, Quaternion.identity, this.transform).GetComponent<GridElement>();
                    instance.position = new Vector2Int(-10 + x, 10 - y);
                }
            }
        }
        Debug.Log("_ObstaclesGenerated!");
    }

    public Vector2Int GetRandomEmptySpace()
    {
        bool validPosition = true;
        Vector2Int newPosition = default;
        int x, y;
        do
        {
            x = Random.Range(startX, endX);
            y = Random.Range(startY, endY);
            validPosition = true;
            foreach (GridElement element in gridElements)
            {
                if (element.position.x == x && element.position.y == y)
                    validPosition = false;
            }
        } while (validPosition==false);

        newPosition = new Vector2Int(x, y);

        return newPosition;
    }
}

public enum EElementType
{
    Null, Snake, Apple, Brick, Food
}
