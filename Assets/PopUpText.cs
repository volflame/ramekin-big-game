using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopUpText : MonoBehaviour
{
    public float jumpPower = 60f;
    public int numJumps = 1;
    public float duration = 1f;     
    public float riseDistance = 70f;
    public Ease easeType = Ease.OutCubic;

    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Play(string message)
    {
        text.text = message;
        canvasGroup.alpha = 1f;
        rectTransform.localScale = Vector3.one;

        // record start position
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, riseDistance);

        // make a tween sequence
        Sequence seq = DOTween.Sequence();
        seq.Append(rectTransform.DOJumpAnchorPos(endPos, jumpPower, numJumps, duration).SetEase(easeType));
        seq.Join(canvasGroup.DOFade(0f, duration).SetEase(Ease.InOutQuad));
        seq.OnComplete(() => Destroy(gameObject));
    }
}
