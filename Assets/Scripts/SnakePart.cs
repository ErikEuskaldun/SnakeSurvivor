using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    public SnakePart nextPart = null;

    public void UpdatePosition(Vector3 position)
    {
        Vector3 oldPosition = transform.position;

        transform.position = position;
        if (nextPart != null)
            nextPart.UpdatePosition(oldPosition);
    }
}
