using System.Collections;
using static UniqueCharacters;

namespace VerticalSlice
{
    [UnityEngine.RequireComponent(typeof(BattleSequence_FirstReviveFinish))]
    public class BattleSequence_FirstHeal : BattleSequence
    {
        private BattleSequence_FirstReviveFinish _reviveFinish;

        private void Awake()
        {
            _reviveFinish = GetComponent<BattleSequence_FirstReviveFinish>();
        }

        public override bool ShouldPlay(BattleEvent e)
        {
            if (e.Type != BattleEvent.EventType.MoveEnd)
            {
                return false;
            }

            bool hasReviveDialoguePlayed = _reviveFinish && _reviveFinish.HasPlayed;
            return !HasPlayed && e.IsHeal && !e.IsRevive && !hasReviveDialoguePlayed;
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            yield return Dialogue.FirstMatchingMainCharacterSay(e.Target,
                "Wow, I feel great! What just happened?",
                "Huh? That was refreshing. What was that?",
                "Woah! Woah! What? I feel like new! What just happened?",
                "Hm? My wounds are all healed. What was that?");

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Interactor,
                "I think the Wisp just used me to heal you!",
                "Not exactly sure, but I think the Wisp used me to heal you.",
                "No idea! I was just following the Wisp, then suddenly I felt energy race through me and into you.",
                "...It seems like the Wisp used me to heal you.");

            yield return Dialogue.FirstMatchingMainCharacterSay(e.Target,
                "What an incredible power! We can use this to beat back the goblins!",
                "Interesting. Working together with this power, we should be able to get the upper hand.",
                "AMAZING! Now we can charge ahead without worrying about taking damage!",
                "This should give us a tactical advantage. We can use this power to beat the goblins and get out of here.");

            if (e.Target == Alfred)
            {
                yield return e.Interactor.Say("(So reckless...)");
            }
        }
    }
}