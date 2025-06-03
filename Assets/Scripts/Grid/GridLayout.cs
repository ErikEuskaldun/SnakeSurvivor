using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridLayout : MonoBehaviour
{
    public TextAsset textAssetData;
    private void Start()
    {
        ReadCSV();
    }
    public string[,] ReadCSV()
    {
        string[] splitY = textAssetData.text.Split("\n");
        string[,] splitXY = new string[splitY.Length, splitY[0].Length/2];
        for (int i = 0; i < splitY.Length; i++)
        {
            string[] splitX = splitY[i].Split(",");
            for (int j = 0; j < splitX.Length; j++)
            {
                splitXY[j,i] = splitX[j];
            }
        }
        return splitXY;
    }
}
