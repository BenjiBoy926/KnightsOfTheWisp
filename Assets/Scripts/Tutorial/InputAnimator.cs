using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputAnimator : MonoBehaviour
{
    [SerializeField] private float _duration = .35f;
    [SerializeField] private Image _optionalImage;
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Sprite _releasedSprite;
    private Finger _finger;
    private FingerTarget _target;

    private void Awake()
    {
        _finger = GetComponentInChildren<Finger>(true);
        _target = GetComponentInChildren<FingerTarget>(true);
    }

    public YieldInstruction Press()
    {
        if (_optionalImage)
        {
            _optionalImage.sprite = _pressedSprite;
        }
        return _finger.Press(_target, _duration);
    }

    public YieldInstruction Release()
    {
        if (_optionalImage)
        {
            _optionalImage.sprite = _releasedSprite;
        }
        return _finger.Release(_target, _duration);
    }

    public void Tap()
    {
        CoroutineRunner.StopAll(this);
        CoroutineRunner.StartCoroutine(this, GetTapSequence());
    }

    private IEnumerator GetTapSequence()
    {
        yield return Press();
        yield return Release();
    }
}