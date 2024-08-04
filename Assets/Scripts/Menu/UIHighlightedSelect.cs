using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHighlightedSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    Selectable ui;
    MenuController menuController;
    private void Start()
    {
        ui = this.GetComponent<Selectable>();
        menuController = GetComponentInParent<MenuController>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.Select();
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (menuController)
            menuController.Lastselected(ui);
    }
}
