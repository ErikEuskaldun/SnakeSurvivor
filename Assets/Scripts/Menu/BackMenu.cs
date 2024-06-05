using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class BackMenu : MonoBehaviour
{
    [SerializeField] bool isActive = true;
    [SerializeField] GameObject backMenu;
    [SerializeField] InputSystemUIInputModule inputsUI;
    private void Start()
    {
        inputsUI.cancel.action.performed += GoBack;
    }

    private void OnEnable()
    {
        isActive = true;
    }
    private void OnDisable()
    {
        isActive = false;
    }

    public void GoBack(InputAction.CallbackContext callbackContext)
    {
        if(!callbackContext.performed || !isActive)
            return;

        backMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
