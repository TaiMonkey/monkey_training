using UnityEngine;
using DG.Tweening;

public class LogoMovement : MonoBehaviour
{
    [SerializeField] private RectTransform logoRectTransform;
    [SerializeField] private Transform targetPoint;
    private const float moveIn = 1f;
    private const float moveOut = 0.5f;

    void Start()
    {
        MoveLogoIn();
    }

    public void MoveLogoOut()
    {
        Vector2 offScreenPosition = new Vector2(Screen.width + logoRectTransform.rect.width, logoRectTransform.anchoredPosition.y);

        logoRectTransform.DOAnchorPos(offScreenPosition, moveOut).SetEase(Ease.InOutQuad);
    }

    public void MoveLogoIn()
    {
        Vector2 offScreenPosition = new Vector2(-logoRectTransform.rect.width, logoRectTransform.anchoredPosition.y);

        logoRectTransform.DOAnchorPos(targetPoint.position, moveIn).SetEase(Ease.InOutQuad);
    }
}

