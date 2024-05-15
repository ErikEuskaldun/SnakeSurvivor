using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPopUp;
    public bool canInteract = true;

    public void GameOver()
    {
        canInteract = false;
        Time.timeScale = 0;
        GameOverPopUp.SetActive(true);
    }
}
