using System.Collections;
using static UniqueCharacters;

namespace VerticalSlice
{
    public class BattleSequence_GrugDeath : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent e)
        {
            return e.Type == BattleEvent.EventType.BeforeMoveEnd && !HasPlayed && (e.Interactor == Grug || e.Target == Grug) && Grug.IsDead;
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            yield return Grug.Say("Gah...");
            yield return Grug.Say("I will dine with Ha'all...");
            yield return Grug.Say("in... paradise... urk...");
        }
    }
}