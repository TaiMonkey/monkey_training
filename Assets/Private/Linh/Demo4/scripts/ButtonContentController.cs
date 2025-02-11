using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonContentController : ButtonController
{
    [SerializeField] private RectTransform rect;
    public Vector3 orgPos;
    protected override void Onclick()

    {
        Debug.LogError("Click button content");
        imageContentController.ChangeImage();
    }
    
}
