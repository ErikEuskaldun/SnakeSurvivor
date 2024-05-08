using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeController))]
public class SnakeVariables : MonoBehaviour
{
    public int lenght = 1;
    public List<SnakePart> snakeParts = new List<SnakePart>();
    public GameObject snakePartPrefab;
    
    public void IncreaseLenght()
    {
        lenght++;
        SnakePart newPart = Instantiate(snakePartPrefab).GetComponent<SnakePart>();
        SnakePart lastPart = snakeParts[snakeParts.Count-1];
        lastPart.nextPart = newPart;
        newPart.UpdatePosition(lastPart.transform.position);
        snakeParts.Add(newPart);
    }

    public void DecreaseLenght()
    {
        if (snakeParts.Count == 1)
            return;
        lenght--;

        SnakePart lastPart = snakeParts[snakeParts.Count - 1];
        snakeParts.Remove(lastPart);
        
        SnakePart newLastPart = snakeParts[snakeParts.Count - 1];
        newLastPart.nextPart = null;

        Destroy(lastPart.gameObject);
    }
}
