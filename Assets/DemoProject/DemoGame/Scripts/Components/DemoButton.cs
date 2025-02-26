using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    internal bool interactable;
    internal object onClick;
    private DemoButton demoButton;
}

public class DemoButton
{
    public GamePlayData gamePlayData { get; set; }
}
