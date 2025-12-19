using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Target Components (auto-assign if empty)")]
    public Image targetImage;
    public RectTransform targetRect;

    [Header("Normal State")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Vector3 normalScale = Vector3.one;

    [Header("Hover State")]
    [SerializeField] private Color hoverColor = new Color(1f, 1f, 1f, 1f); // Brighten in inspector
    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    [SerializeField] private float tweenDuration = 0.2f;

    [Header("Press State")]
    [SerializeField] private Color pressColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    [SerializeField] private Vector3 pressScale = new Vector3(0.95f, 0.95f, 1f);
    [SerializeField] private Ease tweenEase = Ease.OutBack;

    private Tween scaleTween;
    private Tween colorTween;
    private bool isHovered = false;

    void Start()
    {
        // Auto-assign components if not set
        if (targetImage == null) targetImage = GetComponent<Image>();
        if (targetRect == null) targetRect = GetComponent<RectTransform>();

        // Set initial state
        targetImage.color = normalColor;
        targetRect.localScale = normalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        DoHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        DoNormal();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DoPress();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isHovered)
        {
            DoHover();
        }
        else
        {
            DoNormal();
        }
    }

    private void DoNormal()
    {
        KillTweens();
        scaleTween = targetRect.DOScale(normalScale, tweenDuration).SetEase(tweenEase);
        colorTween = targetImage.DOColor(normalColor, tweenDuration).SetEase(tweenEase);
    }

    private void DoHover()
    {
        KillTweens();
        scaleTween = targetRect.DOScale(hoverScale, tweenDuration).SetEase(tweenEase);
        colorTween = targetImage.DOColor(hoverColor, tweenDuration).SetEase(tweenEase);
    }

    private void DoPress()
    {
        KillTweens();
        // Quick press effect
        float pressDuration = tweenDuration * 0.4f;
        scaleTween = targetRect.DOScale(pressScale, pressDuration).SetEase(Ease.OutQuad);
        colorTween = targetImage.DOColor(pressColor, pressDuration).SetEase(Ease.OutQuad);
    }

    private void KillTweens()
    {
        scaleTween?.Kill();
        colorTween?.Kill();
    }

    // Optional: Call this from Button.onClick for extra click feedback
    public void PlayClickEffect()
    {
        DoPress();
        DOVirtual.DelayedCall(tweenDuration * 0.4f, () => {
            if (isHovered) DoHover();
            else DoNormal();
        });
    }

    void OnDestroy()
    {
        KillTweens();
    }
}