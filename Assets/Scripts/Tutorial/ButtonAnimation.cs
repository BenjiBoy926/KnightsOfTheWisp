using DG.Tweening;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private float _duration = .35f;
    private Finger _finger;
    private FingerTarget _target;

    private void Awake()
    {
        _finger = GetComponentInChildren<Finger>(true);
        _target = GetComponentInChildren<FingerTarget>(true);
    }

    public void Press()
    {
        _finger.Press(_target, _duration);
    }

    public void Release()
    {
        _finger.Release(_target, _duration);
    }
}