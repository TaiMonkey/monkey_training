using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class customButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected TMP_Text buttonText;
    [SerializeField] protected Image buttonImage;
    [SerializeField] protected Sprite clickSprite;

    protected Button button;
    private Sprite defaultSprite;

    protected virtual void Start()
    {
        button = GetComponent<Button>();

        if(buttonText == null)
        {
            buttonText = GetComponentInChildren<TMP_Text>();
        }

        if(button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (buttonImage == null)
        {
            buttonImage = GetComponentInChildren<Image>();
        }

        if (buttonImage != null)
        {
            defaultSprite = buttonImage.sprite;
        }
    }

    protected virtual void OnButtonClick()
    {
        if(buttonText != null)
        {
            SetText("Clicked!");
        }
        
        if(buttonImage != null && clickSprite != null) {
            buttonImage.sprite = clickSprite;
        }
        Debug.Log("Button Clicked!");
    }

    public void SetText(string text)
    {
        if(buttonText != null)
        {
            buttonText.text = text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
