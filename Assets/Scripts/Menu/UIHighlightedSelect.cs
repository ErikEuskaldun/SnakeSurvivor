using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHighlightedSelect : MonoBehaviour, IPointerEnterHandler
{
    Button button;
    private void Start()
    {
        button = this.GetComponent<Button>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.Select();
    }
}
