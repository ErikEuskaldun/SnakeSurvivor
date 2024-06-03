using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button defaultSelectedButton;
    bool isPaused = false;
    [SerializeField]PlayerInput inputs;

    private void Start()
    {
        inputs.uiInputModule.cancel.action.performed += Cancel;
    }

    public void ToglePause(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || !GetComponent<GameManager>().canInteract)
            return;

        isPaused = !isPaused;
        if (isPaused)
            Pause();
        else
            Resume();
    }

    public void Cancel(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed)
            return;

        if (isPaused)
            Resume();
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        FindObjectOfType<SnakeController>().inputLocked = true;
        Time.timeScale = 0;
        defaultSelectedButton.Select();
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        SnakeController snake = FindObjectOfType<SnakeController>();
        if (snake.IsGameStarted)
            Time.timeScale = 1;
        snake.inputLocked = false;
    }
}
