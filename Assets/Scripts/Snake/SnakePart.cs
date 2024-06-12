using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakePartSprites))]
public class SnakePart : MonoBehaviour
{
    public SnakePart nextPart = null; //part behind this
    public SnakePart prevPart = null;
    public bool isHead = false;
    private SnakePartSprites sprites;
    [SerializeField]private EDirection direction;

    private void Awake()
    {
        sprites = GetComponent<SnakePartSprites>();
        grid = FindObjectOfType<Grid>();
        gridComponent = GetComponent<GridElement>();
    }

    public void UpdatePosition(Vector3 position) //update to new position and child have now old position
    {
        Vector3 oldPosition = transform.position;

        transform.position = SnakeUtils.RoundFloat(position);
        GridElement gridComponent = GetComponent<GridElement>();
        gridComponent.position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        
        if (nextPart != null)
            nextPart.UpdatePosition(oldPosition);
        if (!isHead)
        {
            ChangeRotation(prevPart.direction);
            GetComponent<SpriteRenderer>().sprite = ChangeSprite();
        }
        else InfiniteLoopMovement();
    }

    Grid grid;
    GridElement gridComponent;
    private void InfiniteLoopMovement()
    {
        Vector3 newPosition = default;
        if (gridComponent.position.x < 1)
            newPosition = new Vector3(grid.xLength-2, transform.position.y);
        else if (gridComponent.position.x > grid.xLength-2)
            newPosition = new Vector3(1, transform.position.y);
        else if (gridComponent.position.y < 1)
            newPosition = new Vector3(transform.position.x, grid.yLength-2);
        else if (gridComponent.position.y > grid.yLength-2)
            newPosition = new Vector3(transform.position.x, 1);

        if (newPosition!=default)
        {
            transform.position = newPosition;
            gridComponent.position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
    }

    public void ChangeSprite(ESnakePart part)
    {
        GetComponent<SpriteRenderer>().sprite = sprites.GetSprite(part);
    }

    public void ChangeRotation(EDirection direction)
    {
        Quaternion newRotation = transform.rotation;
        switch (direction)
        {
            case EDirection.Right:
                newRotation = Quaternion.Euler(0, 0, 180);
                break;
            case EDirection.Left:
                newRotation = Quaternion.Euler(0, 0, 0);
                break;
            case EDirection.Up:
                newRotation = Quaternion.Euler(0, 0, 270);
                break;
            case EDirection.Down:
                newRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
        this.direction = direction;
        transform.rotation = newRotation;
    }

    /*private void ChangeRotationOld(Vector3 oldPosition, Vector3 newPosition)
    {
        Quaternion newRotation = transform.rotation;

        if(oldPosition.x != newPosition.x)
        {
            if (newPosition.x < oldPosition.x)
            {
                newRotation = Quaternion.Euler(0, 0, 0);
                direction = EDirection.Left;
            }
            else
            {
                newRotation = Quaternion.Euler(0, 0, 180);
                direction = EDirection.Right;
            }
        }
        else if(oldPosition.y != newPosition.y)
        {
            if (newPosition.y < oldPosition.y)
            {
                newRotation = Quaternion.Euler(0, 0, 90);
                direction = EDirection.Down;
            }
            else
            {
                newRotation = Quaternion.Euler(0, 0, 270);
                direction = EDirection.Up;
            }
        }
        transform.rotation = newRotation;
    }*/

    private Sprite ChangeSprite()
    {
        GetComponent<SpriteRenderer>().flipY = false;
        if (nextPart == null)
            return sprites.GetSprite(ESnakePart.Tail);

        float xDiference = nextPart.transform.position.x - prevPart.transform.position.x;
        float yDiference = nextPart.transform.position.y - prevPart.transform.position.y;

        if (xDiference != 0 && yDiference != 0)
        {
            TwistedSpriteSprite();
            return sprites.GetSprite(ESnakePart.TwistedBody);
        }
        return sprites.GetSprite(ESnakePart.Body);
    }

    private void TwistedSpriteSprite()
    {
        Quaternion newRotation = transform.rotation;
        if ((nextPart.direction == EDirection.Up && prevPart.direction == EDirection.Right) || 
            (nextPart.direction == EDirection.Right && prevPart.direction == EDirection.Down) ||
            (nextPart.direction == EDirection.Down && prevPart.direction == EDirection.Left) ||
            (nextPart.direction == EDirection.Left && prevPart.direction == EDirection.Up))
            GetComponent<SpriteRenderer>().flipY = true;
    }
}
