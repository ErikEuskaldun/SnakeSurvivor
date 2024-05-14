using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SnakeUtils
{
    public static float TILE_SIZE = 0.16f;
    public static float TILE_MULTIPLIER = 6.25f;

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
}
