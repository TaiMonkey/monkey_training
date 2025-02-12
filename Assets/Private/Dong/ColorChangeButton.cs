using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorChangeButton : customButton
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if( buttonImage == null)
        {
            buttonImage.GetComponent<Image>();
        }
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        if(buttonImage != null)
        {
            buttonImage.color = Color.red;
        }
        Debug.Log("ColorChangeButton Clicked!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
