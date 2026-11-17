using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Finger : MonoBehaviour
{
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public YieldInstruction Press(FingerTarget target, float duration)
    {
        target.Press(duration);
        return transform.DOMove(target.transform.position, duration).WaitForCompletion();
    }

    public YieldInstruction Release(FingerTarget target, float duration)
    {
        target.Release(duration);
        return transform.DOMove(_initialPosition, duration).WaitForCompletion();
    }

    public Coroutine Tap(FingerTarget target, float duration)
    {
        CoroutineRunner.StopAll(this);
        return CoroutineRunner.StartCoroutine(this, GetTapSequence(target, duration));
    }

    private IEnumerator GetTapSequence(FingerTarget target, float duration)
    {
        yield return Press(target, duration);
        yield return Release(target, duration);
    }
}