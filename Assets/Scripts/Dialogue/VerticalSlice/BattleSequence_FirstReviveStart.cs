using System.Collections;

namespace VerticalSlice
{
    public class BattleSequence_FirstReviveStart : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent e)
        {
            return e.Type == BattleEvent.EventType.MoveStart && !HasPlayed && e.IsRevive;
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            yield return Dialogue.FirstMatchingMainCharacterSay(e.Interactor,
                $"Huh? The Wisp... it's leading me over to {e.Target.Name}",
                $"Strange. The Wisp is leading me over to {e.Target.Name}?",
                $"What the heck? Why is the Wisp leading me over to {e.Target.Name}?",
                $"Hm? The Wisp. It's leading me over to {e.Target.Name}.");
        }
    }
}