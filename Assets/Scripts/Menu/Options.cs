using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown ddResolution;
    private void Start()
    {
        resolutions = Screen.resolutions;
        ddResolution.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = default;
        int index = 0;
        foreach (Resolution r in resolutions)
        {
            //Resolution aspectRatio = SnakeUtils.GetAspectRatio(r);
            //string aspectRationString = "(" + aspectRatio.width + ":" + aspectRatio.height + ")";
            string resolutionString = r.width + "X" + r.height;
            if(!options.Contains(resolutionString))
                options.Add(resolutionString);
            if (r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height)
                currentResolutionIndex = index;
            index++;
        }
        ddResolution.AddOptions(options);

        ddResolution.value = currentResolutionIndex;
        ddResolution.RefreshShownValue();
    }

    public void SetResolutuon(int index)
    {
        string resolutionString = ddResolution.options[index].text;
        string[] resolutionSplit = resolutionString.Split("X");
        Resolution resolution = new Resolution();
        resolution.width = int.Parse(resolutionSplit[0]);
        resolution.height = int.Parse(resolutionSplit[1]);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void SetScreenMode(int mode)
    {
        Debug.Log(mode);
        switch (mode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public void SetVSync(bool value)
    {
        if (value == true)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }
}
