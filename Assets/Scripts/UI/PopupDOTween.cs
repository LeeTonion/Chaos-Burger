using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class PopupDOTween : MonoBehaviour
{
   [Header("Configure effects")]
    public float duration = 0f;

    private CanvasGroup canvasGroup;
    private Coroutine currentRoutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ShowEffect());
    }

    private void OnDisable()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        canvasGroup.alpha = 1f;
        transform.localScale = Vector3.one;
    }

    public void HideWithEffect()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(HideEffect());
    }

   private IEnumerator ShowEffect()
{
    canvasGroup.alpha = 0f;
    transform.localScale = Vector3.zero;

    float elapsed = 0f;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.SmoothStep(0f, 1f, elapsed / duration); 

        canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

        yield return null;
    }

    canvasGroup.alpha = 1f;
    transform.localScale = Vector3.one;
}


    private IEnumerator HideEffect()
    {
        float startAlpha = canvasGroup.alpha;
        Vector3 startScale = transform.localScale;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }

        canvasGroup.alpha = 0f;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
}
