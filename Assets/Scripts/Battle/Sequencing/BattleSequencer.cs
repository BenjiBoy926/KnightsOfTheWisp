using System;
using System.Collections;
using UnityEngine;

public class BattleSequencer : MonoBehaviour
{
    public bool IsPlaying => _isPlaying;

    private BattleSequence[] _sequences;
    private bool _isPlaying;

    private void Awake()
    {
        _sequences = GetComponentsInChildren<BattleSequence>(true);
        Array.Sort(_sequences, BattleSequence.Comparison);
    }

    public IEnumerator GetMoveStartSequence(Character interactor, Character target)
    {
        return GetSequence(BattleEvent.TriggerType.MoveStart, interactor, target);
    }

    public IEnumerator GetMoveFinishSequence(Character interactor, Character target)
    {
        return GetSequence(BattleEvent.TriggerType.MoveEnd, interactor, target);
    }

    public IEnumerator GetTurnStartSequence()
    {
        return GetSequence(BattleEvent.TriggerType.TurnStart, null, null);
    }

    public IEnumerator GetUndoRedoEndSequence()
    {
        return GetSequence(BattleEvent.TriggerType.UndoRedoEnd, null, null);
    }

    private IEnumerator GetSequence(BattleEvent.TriggerType trigger, Character interactor, Character target)
    {
        _isPlaying = true;
        foreach (var sequence in _sequences)
        {
            if (sequence.ShouldPlay(trigger, interactor, target))
            {
                yield return sequence.Play(interactor, target);
            }
        }
        _isPlaying = false;
    }
}