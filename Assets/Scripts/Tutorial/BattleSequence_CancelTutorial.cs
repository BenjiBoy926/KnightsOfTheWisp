using System.Collections;

[UnityEngine.RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_CancelTutorial : BattleSequence
{
    private TutorialWindow _window;

    protected override void Awake()
    {
        base.Awake();
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(BattleEvent e)
    {
        return e.Type == BattleEvent.EventType.AfterMoveEnd && !HasPlayed && e.IsAttack && Battle.CurrentFactionTurn == Battle.PlayerFaction;
    }

    protected override IEnumerator GetSequence(BattleEvent e)
    {
        yield return _window.Show();
    }
}