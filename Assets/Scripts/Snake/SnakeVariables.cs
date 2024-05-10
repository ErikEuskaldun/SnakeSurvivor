using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SnakeController))]
public class SnakeVariables : MonoBehaviour
{
    public float speed = 5f;
    public int length = 1;
    public List<SnakePart> snakeParts = new List<SnakePart>();
    [SerializeField] private GameObject snakePartPrefab;
    [SerializeField] private int points = 0;
    [SerializeField] private TMP_Text txtPoints;

    public void StartingLenght(int length)
    {
        if (length < 3)
            length = 3; //min starting length

        for (int i = 1; i < length; i++)
        {
            this.length++;
            SnakePart lastPart = snakeParts[snakeParts.Count - 1];
            SnakePart newPart = Instantiate(snakePartPrefab, lastPart.transform.position + Vector3.right, Quaternion.identity, this.transform).GetComponent<SnakePart>();
            lastPart.nextPart = newPart; //old last part is dad of new part
            newPart.transform.position = lastPart.transform.position + Vector3.right; //new part position is last parts
            snakeParts.Add(newPart);
        }
        
    }

    public void IncreaseLenght() //Increase snake lenght by 1
    {
        this.length++;
        SnakePart lastPart = snakeParts[snakeParts.Count - 1];
        SnakePart newPart = Instantiate(snakePartPrefab, lastPart.transform.position, Quaternion.identity, this.transform).GetComponent<SnakePart>();
        lastPart.nextPart = newPart; //old last part is dad of new part
        newPart.UpdatePosition(lastPart.transform.position); //new part position is last parts
        snakeParts.Add(newPart);
    }

    public void DecreaseLenght() //Decrease snake lenght by 1
    {
        if (snakeParts.Count == 3) //cant be lower than 3
            return;

        length--;

        SnakePart lastPart = snakeParts[snakeParts.Count - 1];
        snakeParts.Remove(lastPart); //remove last part
        
        SnakePart newLastPart = snakeParts[snakeParts.Count - 1];
        newLastPart.nextPart = null; //set to null new last parts child (is last so dont have child)

        Destroy(lastPart.gameObject); //destroy go
    }

    public void IncreasePoints(int value) //points variable controller
    {
        points += value;
        txtPoints.text = "Points: " + points;
    }

    public void GameOver()
    {
        Debug.Log("== GAME OVER ==");
        speed = 0;
    }
}
