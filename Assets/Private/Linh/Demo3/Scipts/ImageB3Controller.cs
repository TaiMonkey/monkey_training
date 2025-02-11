using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageB3Controller : MonoBehaviour
{
    private Image image;
    [SerializeField] private Sprite[] spriteArray;
    private int currentIndex;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangImage()
    {
        if (currentIndex == 0)
            currentIndex = 1;
        else
            currentIndex = 0;
        Sprite sprite = spriteArray[currentIndex];
        image.sprite = sprite;
    }
}
