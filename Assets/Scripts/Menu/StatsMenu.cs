using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text txtSpeed, txtLenght, txtPoints, txtTime, txtHighScore;

    public void UpdateSpeed(float speed)
    {
        txtSpeed.text = "Speed: " + SnakeUtils.RoundFloat(speed);
    }

    public void UpdateLenght(int lenght)
    {
        txtLenght.text = "Lenght: " + lenght;
    }

    public void UpdatePoints(int points)
    {
        txtPoints.text = "Points: " + points;
    }

    public void UpdateHighScore(int points)
    {
        txtHighScore.text = "High Score: " + points;
    }

    public void UpdateTime(int time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        txtTime.text = t.ToString("m':'ss");
    }
}
