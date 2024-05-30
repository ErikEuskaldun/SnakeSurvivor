using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button defaultSelcetedButton;
    void Start()
    {
        defaultSelcetedButton.Select();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Aplication Closed");
    }
}
