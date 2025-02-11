using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected ImageContentController imageContentController;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Onclick);
    }

    protected virtual void Onclick()
    {
        Debug.LogError("Debug in parent");
    }
    
}
