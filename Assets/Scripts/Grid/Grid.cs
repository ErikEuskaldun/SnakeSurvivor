using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<GridElement> gridElements = new List<GridElement>();
    public int xLength, yLength;
    public GameObject floor, brick;
    public Color colorA, colorB;
    void Start()
    {
        GenerateGrid(xLength, yLength);
    }

    public void GenerateGrid(int xLength, int yLength)
    {
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                if (y == 0 || y == yLength-1 || x == 0 || x == xLength-1)
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
        int yInvert = yLength - 1;
        string[,] layout = GetComponent<GridLayout>().ReadCSV();
        for (int x = 0; x < layout.GetLength(0); x++)
        {
            for (int y = 0; y < layout.GetLength(1); y++)
            {
                if (layout[x, y] == "1")
                {
                    Debug.Log((x) + "/" + (yInvert - y));
                    GridElement instance = Instantiate(brick, new Vector3(x, yInvert - y) * SnakeUtils.TILE_SIZE, Quaternion.identity, this.transform).GetComponent<GridElement>();
                    instance.position = new Vector2Int(x, yInvert - y);
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
            x = Random.Range(0, xLength);
            y = Random.Range(0, yLength);
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
