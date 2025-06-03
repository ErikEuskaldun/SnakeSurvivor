using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointFX : MonoBehaviour
{
    [SerializeField] TMP_Text txtPoints;

    public void InstantiatePointsFX(int points)
    {
        int minFont = 5;
        int maxFont = 12;
        int minPoints = 100;
        int maxPoints = 2000;

        txtPoints.text = "+" + points;
        if (points <= minPoints)
            txtPoints.fontSize = minFont;
        else if (points >= maxPoints)
            txtPoints.fontSize = maxFont;
        else
        {
            int pointDiference = (points - minPoints);
            float fontSize = ((float)(maxFont - minFont) * pointDiference / maxPoints) + minFont;
            txtPoints.fontSize = fontSize;
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
