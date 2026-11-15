using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(Overlay))]
public class TutorialWindow : MonoBehaviour
{
    [SerializeField] private float _slideDuration = 0.35f;
    [SerializeField] private float _slideOffset = 100f;
    [SerializeField] private Ease _slideEase = Ease.OutQuint;
    private Overlay _overlay;
    private RectTransform _rectTransform;
    private VideoPlayer _videoPlayer;
    private List<Graphic> _graphicsToFade;
    private ContinueMarker _continueMarker;

    private void Awake()
    {
        _overlay = GetComponent<Overlay>();
        _rectTransform = transform.GetChild(0) as RectTransform;
        _videoPlayer = GetComponentInChildren<VideoPlayer>(true);
        _graphicsToFade = new(GetComponentsInChildren<Graphic>(true));
        for (int i = _graphicsToFade.Count - 1; i >= 0; i--)
        {
            Graphic graphic = _graphicsToFade[i];
            if (graphic.TryGetComponent(out ContinueMarker _))
            {
                _graphicsToFade.RemoveAt(i);
            }
        }

        _continueMarker = GetComponentInChildren<ContinueMarker>(true);
        gameObject.SetActive(false);
    }

    public Coroutine Show()
    {
        gameObject.SetActive(true);
        return CoroutineRunner.Play(this, GetDisplaySequence());
    }

    private IEnumerator GetDisplaySequence()
    {
        _rectTransform.anchoredPosition = new Vector2(0, _slideOffset);
        SetAlpha(0);
        _videoPlayer.Prepare();

        yield return _overlay.FadeIn();

        FadeAlpha(1);
        yield return _rectTransform.DOAnchorPosY(0, _slideDuration)
            .SetEase(_slideEase)
            .WaitForCompletion();

        yield return AwaitOneVideoLoop();

        yield return _continueMarker.Wait();

        FadeAlpha(0);
        yield return _rectTransform.DOAnchorPosY(_slideOffset, _slideDuration)
            .SetEase(_slideEase)
            .WaitForCompletion();

        yield return _overlay.FadeOut();

        gameObject.SetActive(false);
    }

    private IEnumerator AwaitOneVideoLoop()
    {
        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }
        _videoPlayer.Play();
        yield return new WaitUntil(() => _videoPlayer.frameCount > 0);
        yield return new WaitUntil(() => _videoPlayer.frame >= (long)_videoPlayer.frameCount - 1);
    }

    private void SetAlpha(float a)
    {
        foreach (var graphic in _graphicsToFade)
        {
            graphic.color = graphic.color.SetAlpha(a);
        }
    }

    private void FadeAlpha(float a)
    {
        foreach (var graphic in _graphicsToFade)
        {
            graphic.DOFade(a, _slideDuration).SetEase(_slideEase);
        }
    }
}