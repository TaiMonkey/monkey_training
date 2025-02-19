using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDomdomCotroller : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private RectTransform rect;
    
   
    private CFButtonDomDomData databutton;
    [SerializeField] public TMP_Text textContent;
    private void Start()
    {
       
        btn = GetComponent<Button>();
        rect = GetComponent<RectTransform>();

    }
    public void InitData(CFButtonDomDomData data)
    {
        databutton = data;
        textContent.text = data.text;
        textContent.ForceMeshUpdate();

    }
}
