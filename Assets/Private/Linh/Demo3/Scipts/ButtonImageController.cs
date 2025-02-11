using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonImageController : ButtonControllerB3
{
   
    protected override void Onclick()

    {
        Debug.LogError("Click button content");
        imageB3Controller.ChangImage();
    }

}
