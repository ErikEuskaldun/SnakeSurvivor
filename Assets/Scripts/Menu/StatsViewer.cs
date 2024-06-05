using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsViewer : MonoBehaviour
{
    [SerializeField] TMP_Text txtFPS;

    private void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    private void GetFPS()
    {
        float fps = (int)(1f / Time.unscaledDeltaTime);
        txtFPS.text = "FPS: " + fps;
    }
}
