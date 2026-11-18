using System.Collections;

[UnityEngine.RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_CancelTutorial : BattleSequence
{
    public override TriggerType Trigger => TriggerType.MoveEnd;

    private TutorialWindow _window;

    private void Awake()
    {
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(Character interactor, Character target)
    {
        return !HasPlayed && interactor && target && interactor.Faction != target.Faction;
    }

    protected override IEnumerator GetSequence(Character interactor, Character target)
    {
        yield return _window.Show();
    }
}