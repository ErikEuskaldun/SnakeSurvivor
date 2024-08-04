using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeVariables : MonoBehaviour
{
    public float speed = 5f;
    public int length = 1;
    public List<SnakePart> snakeParts = new List<SnakePart>();
    [SerializeField] private GameObject snakePartPrefab;
    [SerializeField] private int points = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int expereince = 0;
    [SerializeField] private int comboMultiplier = 1;
    [SerializeField] private float comboValue = 0;
    private int maxExperience = 100;
    StatsMenu statsUI;

    private GameVariables gameVariables;

    private float LEVEL_XP_MULTIPIER = 1.5f;

    public int Points { get => points; }
    public int ComboForNextLevel { get => comboMultiplier * 200; }
    public int ComboMultiplier { get => comboMultiplier; }

    private void Awake()
    {
        statsUI = FindObjectOfType<StatsMenu>();
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);
        UpdateUIAll();
    }

    private void Start()
    {
        gameVariables = FindObjectOfType<GameVariables>();
        
    }

    private void Update()
    {
        DecreaseCombo();
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
            newPart.InstantiatePart(lastPart);

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
        newPart.InstantiatePart(lastPart);

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
        expereince += Mathf.RoundToInt(value * comboMultiplier * gameVariables.pointMultiplier);
        points += expereince;
        IncreaseCombo(value);

        if(expereince >= maxExperience)
        {
            expereince -= maxExperience;
            level++;
            maxExperience = Mathf.RoundToInt(maxExperience * LEVEL_XP_MULTIPIER);
            FindObjectOfType<UpgradesManager>().UpgradeSelector();
            statsUI.UpdateLevel(level);
        }
        statsUI.UpdatePoints(expereince, maxExperience, points);
    }

    private void IncreaseCombo(int value)
    {
        comboValue += value;
        if(comboValue >= ComboForNextLevel)
        {
            comboValue -= ComboForNextLevel;
            comboMultiplier++;
            statsUI.UpdateComboMultiplier(comboMultiplier);
        }
        statsUI.UpdateComboValue(comboValue, ComboForNextLevel);
    }

    private void DecreaseCombo()
    {
        if(comboMultiplier == 1 && comboValue <= 0)
            return;

        comboValue -= Time.deltaTime * (comboMultiplier * 10);
        if (comboValue < 0)
        {
            if(comboMultiplier == 1)
                comboValue = 0;
            else
            {
                comboMultiplier--;
                comboValue = ComboForNextLevel - comboValue;
                statsUI.UpdateComboMultiplier(comboMultiplier);
            }
        }
        statsUI.UpdateComboValue(comboValue, ComboForNextLevel);
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
        statsUI.UpdatePoints(expereince, maxExperience, points);
        statsUI.UpdateSpeed(speed);
        statsUI.UpdateLenght(length);
        statsUI.UpdateHighScore(PlayerPrefs.GetInt("HighScore"));
        statsUI.UpdateLevel(level);
        statsUI.UpdateComboValue(comboValue, ComboForNextLevel);
        statsUI.UpdateComboMultiplier(comboMultiplier);
    }
}
