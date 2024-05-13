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
    private EDirection direction;

    private void Awake()
    {
        sprites = GetComponent<SnakePartSprites>();
    }

    public void UpdatePosition(Vector3 position) //update to new position and child have now old position
    {
        Vector3 oldPosition = transform.position;

        if(isHead) ChangeRotation(oldPosition, position);

        transform.position = position;
        if (nextPart != null)
        {
            nextPart.UpdatePosition(oldPosition);
            nextPart.ChangeRotation(oldPosition, position);
        }
        if(!isHead) GetComponent<SpriteRenderer>().sprite = ChangeSprite();

    }

    public void ChangeSprite(ESnakePart part)
    {
        GetComponent<SpriteRenderer>().sprite = sprites.GetSprite(part);
    }

    private void ChangeRotation(Vector3 oldPosition, Vector3 newPosition)
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
    }

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
