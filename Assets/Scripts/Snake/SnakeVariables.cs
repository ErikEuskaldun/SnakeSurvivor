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
    [SerializeField] private int level = 1;
    [SerializeField] private int points = 0;
    private int maxPoints = 100;
    StatsMenu statsUI;

    private float LEVEL_XP_MULTIPIER = 1.5f;

    public int Points { get => points; }

    private void Awake()
    {
        statsUI = FindObjectOfType<StatsMenu>();
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);
        UpdateUIAll();
    }

    public void StartingLenght(int length)
    {
        if (length < 3)
            length = 3; //min starting length

        for (int i = 1; i < length; i++)
        {
            this.length++;
            SnakePart lastPart = snakeParts[snakeParts.Count - 1];
            SnakePart newPart = Instantiate(snakePartPrefab, lastPart.transform.position + Vector3.right * SnakeUtils.TILE_SIZE, Quaternion.identity, this.transform).GetComponent<SnakePart>();
            lastPart.nextPart = newPart; //old last part is dad of new part
            newPart.prevPart = lastPart;
            newPart.transform.position = lastPart.transform.position + Vector3.right * SnakeUtils.TILE_SIZE; //new part position is last parts
            
            snakeParts.Add(newPart);
        }
        SnakePart tail = snakeParts[snakeParts.Count - 1];
        tail.ChangeSprite(ESnakePart.Tail);

        statsUI.UpdateLenght(length);
    }

    public void IncreaseLenght() //Increase snake lenght by 1
    {
        this.length++;
        SnakePart lastPart = snakeParts[snakeParts.Count - 1];
        SnakePart newPart = Instantiate(snakePartPrefab, lastPart.transform.position, Quaternion.identity, this.transform).GetComponent<SnakePart>();
        lastPart.nextPart = newPart; //old last part is dad of new part
        newPart.prevPart = lastPart;
        newPart.UpdatePosition(lastPart.transform.position); //new part position is last parts
        newPart.transform.rotation = lastPart.transform.rotation;
        snakeParts.Add(newPart);

        statsUI.UpdateLenght(length);
    }

    public void IncreaseLenght(int value)
    {
        for (int i = 0; i < value; i++)
            IncreaseLenght();
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

        statsUI.UpdateLenght(length);
    }

    public void IncreasePoints(int value) //points variable controller
    {
        points += value;

        if(points>=maxPoints)
        {
            points -= maxPoints;
            level++;
            maxPoints = Mathf.RoundToInt(maxPoints * LEVEL_XP_MULTIPIER);
            FindObjectOfType<UpgradesManager>().TEST_RandomUpgrade();
            statsUI.UpdateLevel(level);
        }

        statsUI.UpdatePoints(points, maxPoints);
    }

    public void IncreaseSpeed(float value)
    {
        speed += value;

        statsUI.UpdateSpeed(speed);
    }

    public void GameOver()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<GameManager>().GameOver();
    }

    public void UpdateUIAll()
    {
        statsUI.UpdatePoints(points, maxPoints);
        statsUI.UpdateSpeed(speed);
        statsUI.UpdateLenght(length);
        statsUI.UpdateHighScore(PlayerPrefs.GetInt("HighScore"));
        statsUI.UpdateLevel(level);
    }
}
