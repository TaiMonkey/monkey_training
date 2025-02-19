using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJarController : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image image;
    private Sprite sprite;
    private CFButtonJarData databutton;
    private void Start()
    {
      
        btn = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
    }
    public void InitData(CFButtonJarData data)
    {
        databutton = data;
        image.sprite = data.imange;
        

    }



}
