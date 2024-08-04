using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Globalization;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text txtSpeed, txtLenght, txtPoints, txtTime, txtHighScore, txtLevel, txtNextLevel, txtComboMultiplier;
    [SerializeField] Slider slXp, slCombo;

    public void UpdateSpeed(float speed)
    {
        txtSpeed.text = "Speed: " + SnakeUtils.RoundFloat(speed);
    }

    public void UpdateLenght(int lenght)
    {
        txtLenght.text = "Lenght: " + lenght;
    }

    public void UpdatePoints(int experience, int maxExpereince, int points)
    {
        string sPoints = points.ToString("#,0", SnakeUtils.GetThousandWithSpaceFormat());
        txtPoints.text = "Points: " + sPoints;
        float slXpValue = (float)experience / maxExpereince;
        slXp.value = slXpValue;
    }

    public void UpdateLevel(int level)
    {
        txtLevel.text = level.ToString();
        txtNextLevel.text = (level + 1).ToString();
    }

    public void UpdateComboMultiplier(int multiplier)
    {
        txtComboMultiplier.text = "X" + multiplier;
        txtComboMultiplier.fontSize = 45 + (multiplier * 5);
    }

    public void UpdateComboValue(float points, int maxPoints)
    {
        float slComboValue = points / maxPoints;
        slCombo.value = slComboValue;
    }

    public void UpdateHighScore(int points)
    {
        txtHighScore.text = "High Score: " + points.ToString("#,0", SnakeUtils.GetThousandWithSpaceFormat());
    }

    public void UpdateTime(int time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        //txtTime.text = time.ToString();
        txtTime.text = t.ToString("mm':'ss");
    }
}
