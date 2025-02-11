using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageContentController : MonoBehaviour , IDragHandler
{
    private Image image;
    private int currentIndex;
    [SerializeField] private Sprite[] spriteArray;
    private RectTransform cacheImangeRectransform;
    [SerializeField] private RectTransform parent;
    
    void Start()
    {
        image = GetComponent<Image>();
        cacheImangeRectransform = GetComponent<RectTransform>();
        parent = cacheImangeRectransform.parent as RectTransform;
       

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 postion = eventData.position;
        Vector2 uiPostion;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, postion, Camera.main, out uiPostion);
        cacheImangeRectransform.anchoredPosition = uiPostion;
    }

    public void ChangeImage()
    {
        if (currentIndex == 0)
            currentIndex = 1;
        else
            currentIndex = 0;
        Sprite sprite = spriteArray[currentIndex];
        image.sprite = sprite;

    }

}
