using System.Collections;
using static UniqueCharacters;

namespace VerticalSlice
{
    public class BattleSequence_FirstReviveFinish : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent e)
        {
            return e.Type == BattleEvent.EventType.MoveEnd && !HasPlayed && e.IsRevive;
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            yield return Dialogue.FirstMatchingMainCharacterSay(e.Interactor,
                "Hey! Are you okay?",
                "Are you alright?",
                "Woah! Are you okay?!",
                "Oh? You're up? Are you alright?");

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Target,
                "Yeah, I think so. I'm pretty exhausted, but I'm not in any pain.",
                "...I think so. I feel exhausted, but I'm not in any pain.",
                "I feel amazing! Although, *yawn, I could really use a nap...",
                "...Yes. I'm tired but my wounds are all healed.");

            if (e.Target == Alfred)
            {
                yield return e.Interactor.Say("Alfred, Focus!");
                yield return e.Target.Say("Right!");
            }

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Target,
                "Was this the Wisp's doing? What happened?",
                "What happened?",
                "Sooooo, what happened?!",
                "What happened?");

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Interactor,
                "It was the Wisp's doing. Somehow it used me to heal you.",
                "I'm not sure. Somehow the Wisp used me to heal you.",
                "Beats me! I felt the Wisp's energy coursing through me, and suddenly you were okay!",
                "I think the Wisp used me to heal you.");

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Target,
                "So it WAS the Wisp!",
                "The Wisp? Remarkable...",
                "So it was the Wisp, huh?",
                "The Wisp? Well... that should help to even the odds.");

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Target,
                "We can win now, I'm sure of it! Arkalux will uphold us with his Wisp!",
                "Using this power, we can work together and cut down this horde!",
                "We can use this power to break through their ranks! Let's go!",
                "Let's use this power to overcome these monsters.");
        }
    }
}