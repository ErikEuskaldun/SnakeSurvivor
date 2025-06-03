using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsViewer : MonoBehaviour
{
    [SerializeField] TMP_Text txtFPS, txtDebug;
    [SerializeField] bool isGameScene = false;

    private void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
        if(isGameScene) InvokeRepeating("GetGameInfo", 1, 1);
    }

    private void GetFPS()
    {
        float fps = (int)(1f / Time.unscaledDeltaTime);
        txtFPS.text = "FPS: " + fps;
    }

    private void GetGameInfo()
    {
        string gameInfo = "pointMultiplier " + GameVariables.pointMultiplier +
            "\nspawnrateMultiplier " + GameVariables.spawnrateMultiplier +
            "\nfoodLifeTimeMultiplier " + GameVariables.foodLifeTimeMultiplier;
        txtDebug.text = gameInfo;
    }
}
