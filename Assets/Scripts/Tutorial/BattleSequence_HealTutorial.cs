using System.Collections;

[UnityEngine.RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_HealTutorial : BattleSequence
{
    private TutorialWindow _window;

    protected override void Awake()
    {
        base.Awake();
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(BattleEvent e)
    {
        return e.Type == BattleEvent.EventType.TurnStart && 
            !HasPlayed && 
            Battle.CurrentFactionTurn == Battle.PlayerFaction && 
            IsAnyPlayerDamaged();
    }

    protected override IEnumerator GetSequence(BattleEvent e)
    {
        yield return _window.Open();
    }

    private bool IsAnyPlayerDamaged()
    {
        foreach (var character in Battle.AllCharacters)
        {
            if (character.Faction == Battle.PlayerFaction && character.CurrentHealth < character.BaseHealth)
            {
                return true;
            }
        }
        return false;
    }
}