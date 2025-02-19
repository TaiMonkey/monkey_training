using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpeaker : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
