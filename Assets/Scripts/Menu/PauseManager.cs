using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button defaultSelectedButton;
    bool isPaused = false;
    [SerializeField]InputSystemUIInputModule inputsUI;
    
    private void Start()
    {
        inputsUI.cancel.action.performed += ToglePause;
    }

    public void ToglePause(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || !GetComponent<GameManager>().canInteract || inputDone)
            return;
        if (callbackContext.action.name == "Cancel" && !isPaused)
            return;

        isPaused = !isPaused;
        if (isPaused)
            Pause();
        else
            Resume();

        StartCoroutine(DobleInputSolver());
    }

    public void Pause()
    {
        isPaused = true;
        defaultSelectedButton.Select();
        pauseMenu.SetActive(true);
        FindObjectOfType<SnakeController>().inputLocked = true;
        Time.timeScale = 0;
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

    private bool inputDone = false;
    IEnumerator DobleInputSolver()
    {
        inputDone = true;
        yield return new WaitForEndOfFrame();
        inputDone = false;
    }
}
