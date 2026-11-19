using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_AttackTutorial : BattleSequence
{
    private TutorialWindow _window;

    private void Awake()
    {
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(BattleEvent.EventType trigger, Character interactor, Character target)
    {
        return trigger == BattleEvent.EventType.TurnStart && !HasPlayed;
    }

    protected override IEnumerator GetSequence(Character interactor, Character target)
    {
        gameObject.SetActive(true);
        yield return _window.Show();
    }
}
