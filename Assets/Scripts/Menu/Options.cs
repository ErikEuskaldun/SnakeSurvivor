using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    //Resolution
    private List<Resolution> resolutions;
    private int currentResolutionIndex = 0;

    public TMP_Dropdown ddResolution;
    public Toggle tgFullScreen, tgVSync;
    public TMP_Text txtDebug;
    private void Start()
    {
        GenerateResolutions();
        SetDefaultOptions();
    }
    private void Update()
    {
        txtDebug.text = "Debug: " + PlayerPrefs.GetInt("VSync");
    }

    private void SetDefaultOptions()
    {
        //FullScreen
        tgFullScreen.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow ? true : false;
        //VSync
        if (!PlayerPrefs.HasKey("VSync"))
            PlayerPrefs.SetInt("VSync", 1);
        tgVSync.isOn = PlayerPrefs.GetInt("VSync") == 1 ? true : false;
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync");
    }

    private void GenerateResolutions()
    {
        Resolution[] fullResolutionList = Screen.resolutions;
        resolutions = new List<Resolution>();

        ddResolution.ClearOptions();
        double currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for(int i=0;i< fullResolutionList.Length;i++)
        {
            if (fullResolutionList[i].refreshRateRatio.value == currentRefreshRate)
                resolutions.Add(fullResolutionList[i]);
        }

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Count; i++)
        {
            string resolutionString = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(resolutionString);
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        ddResolution.AddOptions(options);
        ddResolution.value = currentResolutionIndex;
        ddResolution.RefreshShownValue();
    }

    public void SetResolutuon(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SetVSync(bool isVSyncActivated)
    {
        int vSyncMode = isVSyncActivated ? 1 : 0;
        QualitySettings.vSyncCount = vSyncMode;
        PlayerPrefs.SetInt("VSync", vSyncMode);
    }
}
