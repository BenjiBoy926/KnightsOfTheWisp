using DG.Tweening;
using UnityEngine;

public class Finger : MonoBehaviour
{
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public void Press(FingerTarget target, float duration)
    {
        transform.DOMove(target.transform.position, duration);
        target.Press(duration);
    }

    public void Release(FingerTarget target, float duration)
    {
        transform.DOMove(_initialPosition, duration);
        target.Release(duration);
    }
}