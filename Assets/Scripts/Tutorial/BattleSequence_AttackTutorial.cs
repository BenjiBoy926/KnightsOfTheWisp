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

    public override bool ShouldPlay(BattleEvent e)
    {
        return e.Type == BattleEvent.EventType.TurnStart && !HasPlayed;
    }

    protected override IEnumerator GetSequence(BattleEvent e)
    {
        gameObject.SetActive(true);
        yield return _window.Show();
    }
}
