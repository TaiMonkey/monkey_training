using DG.Tweening;
using MonkeyBase.Observer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monkey.Game.FTS
{
    public class CaAnsController : MonoBehaviour
    {
            [SerializeField] private TextMeshProUGUI label;
            [SerializeField] private RectTransform itemTransform;
            public AudioClip AudioClip { get; set; }
            public bool IsCorrect { get; set; }
            [SerializeField] private int Index;
            private Button button;

            private void Awake()
            {
                button = GetComponent<Button>();
                button.onClick.AddListener(OnClick);
            }

            public void SetLabel(string content)
            {
                label.text = content;
            }

            public void SetScale(Vector3 scale, float time)
            {
                transform.DOScale(scale, time).SetEase(Ease.Linear);
            }

            public void SetScale(Vector3 scale)
            {
                transform.localScale = scale;
            }

            private void OnClick()
            {
                AnswerChanel answerChanel = new AnswerChanel(AnswerChanel.Type.Answer, this);
                ObserverManager.TriggerEvent<AnswerChanel>(answerChanel);
            }
        }
    }

