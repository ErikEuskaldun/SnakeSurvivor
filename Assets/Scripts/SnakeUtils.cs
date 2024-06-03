using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SnakeUtils
{
    public static float TILE_SIZE = 1f;
    public static float TILE_MULTIPLIER = 1f;

    public static float RoundFloat(float value)
    {
        float result = (float)Math.Round(value, 2);
        return result;
    }

    public static Vector3 RoundFloat(Vector3 position)
    {

        float x = RoundFloat(position.x);
        float y = RoundFloat(position.y);
        return new Vector3(x,y);
    }

    public static Vector3 ConvertToRealPosition(Vector2Int position)
    {
        return new Vector3(position.x * TILE_SIZE, position.y * TILE_SIZE);
    }

    public static void ConvertToFixedDirection(ref Vector2 direction)
    {
        direction.Normalize();

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction = direction.x > 0 ? Vector2.right : Vector2.left;
        else
            direction = direction.y > 0 ? Vector2.up : Vector2.down;
    }
}
