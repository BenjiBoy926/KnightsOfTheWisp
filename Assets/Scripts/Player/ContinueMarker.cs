using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class ContinueMarker : MonoBehaviour
{
    public bool Continue => !ShouldUseAutoContinue() && _continue;

    [SerializeField] private InputActionReference _continueAction;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private bool _autoContinue = false;
    private Graphic _graphic;
    private bool _continue = false;
    private float _autoContinueStartTime = 0;

    private void Awake()
    {
        _graphic = GetComponent<Graphic>();
        SetAlpha(0);
    }

    private void OnEnable()
    {
        _continueAction.action.performed += OnContinue;
    }

    private void OnDisable()
    {
        _continueAction.action.performed -= OnContinue;
    }

    private void OnContinue(InputAction.CallbackContext obj)
    {
        _continue = true;
    }

    public Coroutine Wait()
    {
        CoroutineRunner.StopAll(this);
        return CoroutineRunner.Run(this, WaitSequence());
    }

    private IEnumerator WaitSequence()
    {
        SetAlpha(0);
        _graphic.DOFade(1, _fadeDuration).SetLoops(-1, LoopType.Yoyo);

        Listen();
        yield return new WaitUntil(ShouldContinue);

        _graphic.DOKill();
        SetAlpha(0);
    }

    public void Listen()
    {
        _continue = false;
        _autoContinueStartTime = Time.time;
    }

    private void SetAlpha(float alpha)
    {
       _graphic.color = _graphic.color.SetAlpha(alpha);
    }

    private bool ShouldContinue()
    {
        return (ShouldUseAutoContinue() && IsAutoContinueTimerFinished()) || (!ShouldUseAutoContinue() && _continue);
    }

    private bool ShouldUseAutoContinue()
    {
        return _autoContinue && Application.isEditor;
    }

    private bool IsAutoContinueTimerFinished()
    {
        float elapsed = Time.time - _autoContinueStartTime;
        return elapsed >= 1;
    }
}