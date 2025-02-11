using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnsController : ButtonControllerB3, IDragHandler
{
    [SerializeField] private ButtonConfigB3 buttonConfigB3;
    [SerializeField] private Image imageShadow;
    [SerializeField] private Image imageBackground;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform rect;

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        rect.localPosition = localPoint;
    }

    protected override void Onclick()
    {
        Debug.Log("click button dap an");
        text.SetText("Li");
        text.color = Color.black;
        imageShadow.color = buttonConfigB3.buttonFalse.colorShadow;
        imageBackground.color = buttonConfigB3.buttonFalse.colorShadow;
    }

    
}
