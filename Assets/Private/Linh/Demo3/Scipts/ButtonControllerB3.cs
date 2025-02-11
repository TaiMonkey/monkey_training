using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControllerB3 : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected ImageB3Controller imageB3Controller;

   protected virtual void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Onclick);
    }
    protected virtual void Onclick()
    {
        Debug.LogError("click form parent");

    }
    
        
}
