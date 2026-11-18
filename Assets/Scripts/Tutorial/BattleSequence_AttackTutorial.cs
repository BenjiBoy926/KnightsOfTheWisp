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

    public override bool ShouldPlay(TriggerType trigger, Character interactor, Character target)
    {
        return trigger == TriggerType.TurnStart && !HasPlayed;
    }

    protected override IEnumerator GetSequence(Character interactor, Character target)
    {
        gameObject.SetActive(true);
        yield return _window.Show();
    }
}
