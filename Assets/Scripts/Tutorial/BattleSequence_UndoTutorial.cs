using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_UndoTutorial : BattleSequence
{
    [SerializeField] private BattleSequence_HealTutorial _healTutorial;
    private TutorialWindow _window;

    protected override void Awake()
    {
        base.Awake();
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(BattleEvent e)
    {
        return e.Type == BattleEvent.EventType.TurnStart && !HasPlayed && _healTutorial.HasPlayed && Battle.CurrentFactionTurn == Battle.PlayerFaction;
    }

    protected override IEnumerator GetSequence(BattleEvent e)
    {
        yield return _window.Open();
    }
}