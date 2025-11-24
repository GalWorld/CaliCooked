using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum UIAnimationType
{
    Fade,
    Scale,
    FadeAndScale
}

[System.Serializable]
public class UIAnimationSettings
{
    public UIAnimationType animationType = UIAnimationType.FadeAndScale;

    [Header("Duraciones")]
    public float duration = 0.35f;

    [Header("Fade")]
    public float startAlpha = 0f;
    public float endAlpha = 1f;

    [Header("Scale")]
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;

    [Header("Otros")]
    public Ease ease = Ease.OutBack;
}

public class UIPanelAnimator : MonoBehaviour
{
    [Header("Requeridos")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool AutoActivate = false;

    [Header("Entrada")]
    public UIAnimationSettings entryAnimation;

    [Header("Salida")]
    public UIAnimationSettings exitAnimation;

    private RectTransform rect;
    private Vector3 originalScale;
    private float originalAlpha;

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
        originalAlpha = canvasGroup.alpha;
    }

    public void ActivateGO()
    {
        gameObject.SetActive(true);
        Play(entryAnimation);
    }
    public void DesactivateGO()
    {
        Play(exitAnimation).OnComplete(() =>
{
    // Restaurar valores ANTES de apagar
    rect.localScale = originalScale;
    canvasGroup.alpha = originalAlpha;

    gameObject.SetActive(false);
});
    }
    private void OnEnable() {
        if (AutoActivate)
        {
            Play(entryAnimation);
        }
    }
    private void OnDisable() {
        if (AutoActivate)
        {
            Play(exitAnimation);
        }
    }


    public Tween Play(UIAnimationSettings settings)
    {
        DOTween.Kill(rect);
        DOTween.Kill(canvasGroup);

        Sequence seq = DOTween.Sequence();

        switch (settings.animationType)
        {
            case UIAnimationType.Fade:
                seq.Append(canvasGroup.DOFade(settings.endAlpha, settings.duration));
                canvasGroup.alpha = settings.startAlpha;
                break;

            case UIAnimationType.Scale:
                rect.localScale = settings.startScale;
                seq.Append(rect.DOScale(settings.endScale, settings.duration).SetEase(settings.ease));
                break;

            case UIAnimationType.FadeAndScale:
                rect.localScale = settings.startScale;
                canvasGroup.alpha = settings.startAlpha;

                seq.Join(rect.DOScale(settings.endScale, settings.duration).SetEase(settings.ease));
                seq.Join(canvasGroup.DOFade(settings.endAlpha, settings.duration));
                break;
        }

        return seq;
    }
}
