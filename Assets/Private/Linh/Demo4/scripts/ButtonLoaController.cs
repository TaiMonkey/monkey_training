using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonLoaController : ButtonController
{
    [SerializeField] private TextMeshProUGUI text;
        protected override void Onclick()
    {
        Debug.LogError("Click loa");
        text.SetText("Hello, I'm Link");
        text.color = Color.red;
    }
}

