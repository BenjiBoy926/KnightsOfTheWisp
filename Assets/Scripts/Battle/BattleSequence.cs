using System;
using System.Collections;
using UnityEngine;

public abstract class BattleSequence : MonoBehaviour
{
    // TODO: add a distinction between move end before target dies and move end after they die
    public enum TriggerType
    {
        MoveStart,
        MoveEnd,
        TurnStart,
        UndoRedoEnd
    }

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

    // TODO: pack all three parameters into one parameter object with some handy properties and methods
    public abstract bool ShouldPlay(TriggerType trigger, Character interactor, Character target);
    public IEnumerator Play(Character interactor, Character target)
    {
        _timesPlayed++;
        return GetSequence(interactor, target);
    }
    protected abstract IEnumerator GetSequence(Character interactor, Character target);

    protected void Cancel()
    {
        _timesPlayed--;
    }
}
