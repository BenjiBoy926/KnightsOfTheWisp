using System.Collections;
using static UniqueCharacters;

namespace VerticalSlice
{
    public class BattleSequence_GrugKill : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent e)
        {
            if (e.Type != BattleEvent.EventType.MoveEnd)
            {
                return false;
            }

            bool isSomeoneGrug = Grug == e.Interactor || Grug == e.Target;
            bool isSomeoneDeadPlayerCharacter = IsDeadPlayer(e.Interactor) || IsDeadPlayer(e.Target);
            return !HasPlayed && Grug.IsAlive && isSomeoneGrug && isSomeoneDeadPlayerCharacter;
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            yield return Grug.Say("Grahahaaa!");
            yield return Grug.Say("Pathetic! You deserve only subjugation under Ha'all and his hordes!");
        }

        private bool IsDeadPlayer(Character character)
        {
            return character && character.Faction == Battle.PlayerFaction && character.IsDead;
        }
    }
}