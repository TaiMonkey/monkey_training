using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class createButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform layoutGroup;
    [SerializeField] private customButton buttonPrefab;
    [SerializeField] private ColorChangeButton colorChangePrefab;
    void Start()
    {
        CreateButton();
    }

    void CreateButton()
    {
        for(int i = 1; i <= 3; i++)
        {
            customButton objButton = Instantiate(buttonPrefab, layoutGroup, false);
            objButton.name = "Button " + i;
            objButton.SetText("Button abc xyz" + i);

            ColorChangeButton button = Instantiate(colorChangePrefab, layoutGroup, false);
            button.name = "color " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
