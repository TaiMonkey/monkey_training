using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.MTCCA
{
    public class ButtonAnsManagerController : MonoBehaviour
    {
        [SerializeField] private  GameObject buttonAns;
        private VerticalLayoutGroup  vertical;

        private List<ButtonAnsController> listButton;
        private Coroutine coroutine;
       
    }

}
