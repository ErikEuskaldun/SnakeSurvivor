using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text txtSpeed, txtLenght, txtPoints, txtTime, txtHighScore, txtLevel, txtNextLevel;
    [SerializeField] Slider slXp;

    public void UpdateSpeed(float speed)
    {
        txtSpeed.text = "Speed: " + SnakeUtils.RoundFloat(speed);
    }

    public void UpdateLenght(int lenght)
    {
        txtLenght.text = "Lenght: " + lenght;
    }

    public void UpdatePoints(int points, int maxPoints)
    {
        txtPoints.text = "Points: " + points;
        float slXpValue = (float)points / maxPoints;
        slXp.value = slXpValue;
    }

    public void UpdateLevel(int level)
    {
        txtLevel.text = level.ToString();
        txtNextLevel.text = (level + 1).ToString();
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
