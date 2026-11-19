using System.Collections;

[UnityEngine.RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_CancelTutorial : BattleSequence
{
    private TutorialWindow _window;

    private void Awake()
    {
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(BattleEvent e)
    {
        return e.Type == BattleEvent.EventType.MoveEnd && !HasPlayed && e.Interactor && e.Target && e.Interactor.Faction != e.Target.Faction;
    }

    protected override IEnumerator GetSequence(BattleEvent e)
    {
        yield return _window.Show();
    }
}