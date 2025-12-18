using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WipeController : MonoBehaviour
{
    private Image _image;
    private Material _mat;

    private readonly int _circleSizeId = Shader.PropertyToID("_Circle_Size");

    public float minSize = 0f;      // lỗ nhỏ
    public float maxSize = 2f;      // mở full
    public float duration = 0.6f;

    private bool _isIn = false;
    private Tween _tween;

    public static WipeController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _image = GetComponent<Image>();

        // Clone material để tránh ảnh hưởng UI khác
        _mat = Instantiate(_image.material);
        _image.material = _mat;

        _mat.SetFloat(_circleSizeId, minSize);
        AnimateIn();
    }

    public void AnimateIn()
    {
        _tween?.Kill();

        _tween = DOTween.To(
            () => _mat.GetFloat(_circleSizeId),
            x => _mat.SetFloat(_circleSizeId, x),
            maxSize,
            duration
        );

        _isIn = true;
    }

    public void AnimateOut(System.Action onComplete)
{
    _tween?.Kill();

    _tween = DOTween.To(
        () => _mat.GetFloat(_circleSizeId),
        x => _mat.SetFloat(_circleSizeId, x),
        minSize,
        duration
    ).SetEase(Ease.InOutQuad)
     .OnComplete(() => onComplete?.Invoke());
}


    
}
