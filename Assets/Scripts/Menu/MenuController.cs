using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    bool isActive = true;
    [SerializeField] GameObject backMenu;
    [SerializeField] InputSystemUIInputModule inputsUI;
    [SerializeField] Selectable defaultSelected;
    [SerializeField] bool autoSelectLast = false;
    [SerializeField] Selectable lastSelected;
    private void Start()
    {
        inputsUI.cancel.action.performed += GoBack;
    }

    public void Lastselected(Selectable lastSelected)
    {
        if (!autoSelectLast)
            return;
        this.lastSelected = lastSelected;
    }

    private void DefaultSelected()
    {
        if(lastSelected != null)
            lastSelected.Select();
        else
            defaultSelected.Select();
    }

    private void OnEnable()
    {
        isActive = true;
        DefaultSelected();
    }
    private void OnDisable()
    {
        isActive = false;
    }

    public void GoBack(InputAction.CallbackContext callbackContext)
    {
        if(!callbackContext.performed || !isActive || backMenu==null)
            return;

        backMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
