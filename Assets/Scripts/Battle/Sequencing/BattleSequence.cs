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

    protected virtual void Awake()
    {
        _battle = GetComponentInParent<Battle>();
    }

    public abstract bool ShouldPlay(BattleEvent e);
    public IEnumerator Play(BattleEvent e)
    {
        if (!enabled && Application.isEditor)
            return null;

        _timesPlayed++;
        return GetSequence(e);
    }
    protected abstract IEnumerator GetSequence(BattleEvent e);

    protected void Cancel()
    {
        _timesPlayed--;
    }

    protected Character GetPlayer(BattleEvent e)
    {
        if (e.Interactor && e.Interactor.Faction == _battle.PlayerFaction)
        {
            return e.Interactor;
        }
        if (e.Target && e.Target.Faction == _battle.PlayerFaction)
        {
            return e.Target;
        }
        return null;
    }

    protected Character GetEnemy(BattleEvent e)
    {
        if (e.Interactor && e.Interactor.Faction != _battle.PlayerFaction)
        {
            return e.Interactor;
        }
        if (e.Target && e.Target.Faction != _battle.PlayerFaction)
        {
            return e.Target;
        }
        return null;
    }
}
