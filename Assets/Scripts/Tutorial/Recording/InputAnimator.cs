using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputAnimator : MonoBehaviour
{
    [SerializeField] private float _duration = .35f;
    [SerializeField] private Image _optionalImage;
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Sprite _releasedSprite;
    private InputHand _hand;
    private InputHandTarget _target;

    private void Awake()
    {
        _hand = GetComponentInChildren<InputHand>(true);
        _target = GetComponentInChildren<InputHandTarget>(true);
    }

    public YieldInstruction Press()
    {
        if (_optionalImage)
        {
            _optionalImage.sprite = _pressedSprite;
        }
        return _hand.Press(_target, _duration);
    }

    public YieldInstruction Release()
    {
        if (_optionalImage)
        {
            _optionalImage.sprite = _releasedSprite;
        }
        return _hand.Release(_target, _duration);
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