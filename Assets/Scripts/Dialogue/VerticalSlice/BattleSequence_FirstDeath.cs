using System.Collections;
using static UniqueCharacters;

namespace VerticalSlice
{
    public class BattleSequence_FirstDeath : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent e)
        {
            return e.Type == BattleEvent.EventType.MoveEnd && !HasPlayed && (IsDeadPlayerCharacter(e.Interactor) || IsDeadPlayerCharacter(e.Target));
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            Character deadPlayer = IsDeadPlayerCharacter(e.Interactor) ? e.Interactor : e.Target;

            string exclamation1 = $"{deadPlayer.Name}?";
            string exclamation2 = $"{deadPlayer.Name.ToUpper()}!! NO!!";

            yield return Dialogue.FirstLiveCharacterSay(
                new(Hayden, exclamation1),
                new(Alfred, exclamation1));

            yield return Dialogue.FirstLiveCharacterSay(
                new(Hayden, exclamation2),
                new(Alfred, exclamation2));

            yield return Dialogue.FirstLiveCharacterSay(
                new(Gregory, "Damn it..."),
                new(Robin, "Damn it..."));

            yield return Dialogue.FirstLiveCharacterSay(
                new(Robin, "We have to turn back. They are too strong for us"),
                new(Alfred, "What do we do? We can't just leave him!"));

            if (Robin.IsAlive)
            {
                yield return Dialogue.FirstLiveCharacterSay(
                    new(Alfred, "We can't just leave him!"),
                    new(Gregory, "No. We cannot leave him."));

                yield return Dialogue.FirstLiveCharacterSay(
                    new(Hayden, "He's right. We won't retreat."),
                    new(Gregory, "He's right. We must stick together. We must fight on!"));

                if (Hayden.IsAlive)
                {
                    yield return Hayden.Say("Arkalux help us...");
                }
            }
            else
            {
                yield return Gregory.Say("Right. We must stick together. We must fight on!");
                yield return Hayden.Say("Arkalux help us...");
            }
        }

        private bool IsDeadPlayerCharacter(Character character)
        {
            return character && character.Faction == Battle.PlayerFaction && character.IsDead;
        }
    }
}