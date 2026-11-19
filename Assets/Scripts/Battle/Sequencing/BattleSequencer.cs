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

    public IEnumerator GetSequence(BattleEvent e)
    {
        _isPlaying = true;
        foreach (var sequence in _sequences)
        {
            if (sequence.ShouldPlay(e))
            {
                yield return sequence.Play(e);
            }
        }
        _isPlaying = false;
    }
}