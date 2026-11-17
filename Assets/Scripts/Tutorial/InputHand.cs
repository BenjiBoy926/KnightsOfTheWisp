using DG.Tweening;
using System.Collections;
using UnityEngine;

public class InputHand : MonoBehaviour
{
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public YieldInstruction Press(InputHandTarget target, float duration)
    {
        target.Press(duration);
        return transform.DOMove(target.transform.position, duration).WaitForCompletion();
    }

    public YieldInstruction Release(InputHandTarget target, float duration)
    {
        target.Release(duration);
        return transform.DOMove(_initialPosition, duration).WaitForCompletion();
    }
}