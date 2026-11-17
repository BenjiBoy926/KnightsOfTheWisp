using DG.Tweening;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration = .35f;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    public YieldInstruction Press()
    {
        return transform.DOMove(_target.position, _duration).WaitForCompletion();
    }

    public YieldInstruction Release()
    {
        return transform.DOMove(_initialPosition, _duration).WaitForCompletion();
    }
}