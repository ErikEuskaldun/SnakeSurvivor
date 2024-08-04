using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Aplication Closed");
    }
}
