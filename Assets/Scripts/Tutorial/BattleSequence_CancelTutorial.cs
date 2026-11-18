using System.Collections;

[UnityEngine.RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_CancelTutorial : BattleSequence
{
    private TutorialWindow _window;

    private void Awake()
    {
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(TriggerType trigger, Character interactor, Character target)
    {
        return trigger == TriggerType.MoveEnd && !HasPlayed && interactor && target && interactor.Faction != target.Faction;
    }

    protected override IEnumerator GetSequence(Character interactor, Character target)
    {
        yield return _window.Show();
    }
}