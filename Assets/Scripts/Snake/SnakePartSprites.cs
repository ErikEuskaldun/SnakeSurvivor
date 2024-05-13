using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePartSprites : MonoBehaviour
{
    [SerializeField] private List<Sprite> snakeSprites = new List<Sprite>();

    public Sprite GetSprite(ESnakePart part)
    {
        switch (part)
        {
            case ESnakePart.Head:
                return snakeSprites[0];
            case ESnakePart.Body:
                return snakeSprites[2];
            case ESnakePart.TwistedBody:
                return snakeSprites[1];
            case ESnakePart.Tail:
                return snakeSprites[3];
        }
        return snakeSprites[2];
    }
}

public enum ESnakePart
{
    Head, Body, TwistedBody, Tail
}