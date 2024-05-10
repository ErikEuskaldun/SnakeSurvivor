using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    public SnakePart nextPart = null; //part behind this

    public void UpdatePosition(Vector3 position) //update to new position and child have now old position
    {
        Vector3 oldPosition = transform.position;

        transform.position = position;
        if (nextPart != null)
            nextPart.UpdatePosition(oldPosition);
    }
}
