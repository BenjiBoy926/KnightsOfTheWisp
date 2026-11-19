using System;
using System.Collections;
using UnityEngine;

public abstract class BattleSequence : MonoBehaviour
{    
    protected Battle Battle => _battle;
    public bool HasPlayed => _timesPlayed > 0;
    public static readonly Comparison<BattleSequence> Comparison = (a, b) => a._executionOrder.CompareTo(b._executionOrder);

    [SerializeField] private int _executionOrder = 0;
    private Battle _battle;
    private int _timesPlayed = 0;

    private void Awake()
    {
        _battle = GetComponentInParent<Battle>();
    }

    public abstract bool ShouldPlay(BattleEvent e);
    public IEnumerator Play(BattleEvent e)
    {
        _timesPlayed++;
        return GetSequence(e);
    }
    protected abstract IEnumerator GetSequence(BattleEvent e);

    protected void Cancel()
    {
        _timesPlayed--;
    }
}
