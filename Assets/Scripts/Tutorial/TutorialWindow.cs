using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(Overlay))]
public class TutorialWindow : MonoBehaviour
{
    [SerializeField] private TutorialData _data;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;

    [Space]
    [SerializeField] private float _slideDuration = 0.35f;
    [SerializeField] private float _slideOffset = 100f;
    [SerializeField] private Ease _slideEase = Ease.OutQuint;

    private Overlay _overlay;
    private RectTransform _rectTransform;
    private VideoPlayer _videoPlayer;
    private RawImage _videoImage;
    private List<Graphic> _graphicsToFade;
    private ContinueMarker _continueMarker;

    private void Awake()
    {
        _overlay = GetComponent<Overlay>();
        _rectTransform = transform.GetChild(0) as RectTransform;
        _videoPlayer = GetComponentInChildren<VideoPlayer>(true);
        _videoImage = GetComponentInChildren<RawImage>(true);
        _graphicsToFade = new(GetComponentsInChildren<Graphic>(true));
        _continueMarker = GetComponentInChildren<ContinueMarker>(true);

        _title.text = _data.Title;
        _videoPlayer.url = _data.VideoUrl;
        _description.text = _data.Description;
        for (int i = _graphicsToFade.Count - 1; i >= 0; i--)
        {
            Graphic graphic = _graphicsToFade[i];
            if (graphic.TryGetComponent(out ContinueMarker _))
            {
                _graphicsToFade.RemoveAt(i);
            }
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public Coroutine Show()
    {
        gameObject.SetActive(true);
        return CoroutineRunner.StartCoroutine(this, GetDisplaySequence());
    }

    private IEnumerator GetDisplaySequence()
    {
        _rectTransform.anchoredPosition = new Vector2(0, _slideOffset);
        SetAlpha(0);
        _videoPlayer.Prepare();
        _videoImage.enabled = false;

        _overlay.FadeIn();
        FadeAlpha(1);
        yield return _rectTransform.DOAnchorPosY(0, _slideDuration)
            .SetEase(_slideEase)
            .WaitForCompletion();

        yield return AwaitOneVideoLoop();

        yield return _continueMarker.Wait();

        _overlay.FadeOut();
        FadeAlpha(0);
        yield return _rectTransform.DOAnchorPosY(_slideOffset, _slideDuration)
            .SetEase(_slideEase)
            .WaitForCompletion();

        gameObject.SetActive(false);
    }

    private IEnumerator AwaitOneVideoLoop()
    {
        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }
        _videoImage.enabled = true;
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