public class BattleEvent
{
    public enum EventType
    {
        MoveStart,
        MoveEnd,
        TurnStart,
        UndoRedoEnd
    }

    public bool IsAttack => Interactor && Target && Interactor.Faction != Target.Faction;
    public bool IsHeal => Interactor && Target && Interactor.Faction == Target.Faction;
    public bool IsRevive => IsHeal && Target.GetLastRecordedState().Health <= 0;

    public readonly EventType Type;
    public readonly Character Interactor;
    public readonly Character Target;

    public static BattleEvent MoveStart(Character interactor, Character target)
    {
        return new(EventType.MoveStart, interactor, target);
    }

    public static BattleEvent MoveEnd(Character interactor, Character target)
    {
        return new(EventType.MoveEnd, interactor, target);
    }

    public static BattleEvent TurnStart()
    {
        return new(EventType.TurnStart, null, null);
    }

    public static BattleEvent UndoRedoEnd()
    {
        return new(EventType.UndoRedoEnd, null, null);
    }

    private BattleEvent(EventType type, Character interactor, Character target)
    {
        Type = type;
        Interactor = interactor;
        Target = target;
    }
}