using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPopUp;
    [SerializeField] GameObject GameWonPopUp;
    public bool canInteract = true;
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
        canInteract = false;
        Time.timeScale = 0;
        GameOverPopUp.SetActive(true);

        CheckHighScore();
    }

    public void GameWon()
    {
        canInteract = false;
        Time.timeScale = 0;
        GameWonPopUp.SetActive(true);

        CheckHighScore();
    }

    private void CheckHighScore()
    {
        int points = FindObjectOfType<SnakeVariables>().Points;
        if (points > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", points);
    }


}
