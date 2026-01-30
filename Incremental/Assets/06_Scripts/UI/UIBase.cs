using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup panel;
    [SerializeField] protected float fadeDuration = 0.1f;

    private void Start()
    {
        InstantHide();
    }

    private void OnDestroy()
    {
        panel.DOKill();
    }

    public virtual void Show()
    {
        panel.DOKill();
        panel.gameObject.SetActive(true);
        panel.alpha = 0;
        panel.blocksRaycasts = true;
        panel.DOFade(1, fadeDuration).SetUpdate(true); // Fade in effect
    }

    public void ShowSmooth()
    {
        panel.DOKill();
        panel.gameObject.SetActive(true);
        panel.DOFade(1, fadeDuration).SetUpdate(true); // Fade in effect
    }

    public virtual void Show(Action onCompleted)
    {
        panel.DOKill();
        panel.gameObject.SetActive(true);
        panel.alpha = 0;
        panel.blocksRaycasts = true;
        panel.DOFade(1, fadeDuration).OnComplete(() => onCompleted?.Invoke()).SetUpdate(true); // Fade in effect
    }

    public virtual void InstantShow()
    {
        panel.gameObject.SetActive(true);
        panel.alpha = 1;
        panel.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        panel.DOKill();
        // panel.alpha = 1;
        panel.DOFade(0, fadeDuration).OnComplete(() =>
        {
            InstantHide();
        }).SetUpdate(true);
    }

    public void InstantHide()
    {
        panel.DOKill();
        panel.gameObject.SetActive(false);
        panel.blocksRaycasts = false;
    }
}
