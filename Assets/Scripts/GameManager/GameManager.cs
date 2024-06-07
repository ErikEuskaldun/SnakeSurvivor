using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPopUp;
    [SerializeField] Button GameOverDefaultButton;
    [SerializeField] GameObject GameWonPopUp;
    [SerializeField] Button GameWonDefaultButton;
    public bool isLockedMenuing = false;
    public int timer = 300;
    [SerializeField] private StatsMenu statsMenu;
    [SerializeField] private UpgradesManager upgradesController;

    private void Start()
    {
        statsMenu.UpdateTime(timer);
    }

    private void Update()
    {
        TimeController();
    }

    float second = 0;
    private void TimeController()
    {
        second += Time.deltaTime;
        if (second > 1f)
        {
            second -= 1f;
            timer -= 1;
            statsMenu.UpdateTime(timer);
        }
        if (timer <= 0)
            GameWon();
    }

    public void GameOver()
    {
        isLockedMenuing = true;
        Time.timeScale = 0;
        GameOverPopUp.SetActive(true);
        GameOverDefaultButton.Select();

        CheckHighScore();
    }

    public void LockInteraction()
    {
        isLockedMenuing = true;
        FindObjectOfType<SnakeController>().inputLocked = true;
        Time.timeScale = 0;
    }

    public void ResumeInteraction()
    {
        SnakeController snake = FindObjectOfType<SnakeController>();
        isLockedMenuing = false;
        if(!snake.isPlayerStill)
            Time.timeScale = 1;
        snake.inputLocked = false;
    }

    public void GameWon()
    {
        isLockedMenuing = true;
        Time.timeScale = 0;
        GameWonPopUp.SetActive(true);
        GameWonDefaultButton.Select();

        CheckHighScore();
    }

    private void CheckHighScore()
    {
        int points = FindObjectOfType<SnakeVariables>().Points;
        if (points > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", points);
    }


}
