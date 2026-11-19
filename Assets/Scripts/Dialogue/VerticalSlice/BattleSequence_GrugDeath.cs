using System.Collections;
using static UniqueCharacters;

namespace VerticalSlice
{
    public class BattleSequence_GrugDeath : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent.TriggerType trigger, Character interactor, Character target)
        {
            return trigger == BattleEvent.TriggerType.MoveEnd && !HasPlayed && (interactor == Grug || target == Grug) && Grug.IsDead;
        }

        protected override IEnumerator GetSequence(Character interactor, Character target)
        {
            yield return Grug.Say("Gah...");
            yield return Grug.Say("I will dine with Ha'all...");
            yield return Grug.Say("in... paradise... urk...");
        }
    }
}