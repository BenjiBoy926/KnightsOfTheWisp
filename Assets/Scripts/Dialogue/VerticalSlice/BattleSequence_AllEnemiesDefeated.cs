using System.Collections;

namespace VerticalSlice
{
    public class BattleSequence_AllEnemiesDefeated : BattleSequence
    {
        public override bool ShouldPlay(BattleEvent e)
        {
            return e.Type == BattleEvent.EventType.MoveEnd && !HasPlayed && Battle.AreAllEnemiesDead() && !Battle.AreAllPlayersAlive();
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            Character player = IsPlayerCharacter(e.Interactor) ? e.Interactor : e.Target;

            string[] line1 = new string[]
            {
                "Ha... Ha... that was hard-fought...",
                "Ha... Ha... that was a tough fight...",
                "Ha... Ha... finally! Tough... opponents...",
                "Ha... Ha... so much trouble to follow a stupid blue fire..."
            };
            string[] line2 = new string[]
            {
                "But in the end... we prevailed! Ha...",
                "But working together... we finally pulled through... Ha...",
                "But clearly... we're still tougher! Ha...",
                "Whatever. At least we finally outsmarted them... Ha..."
            };
            string[] line3 = new string[]
            {
                "We'd better get patched up. Thank Arkalux for the Wisp's healing or we'd be done for!",
                "We'd better get patched up and move out before reinforcements come.",
                "We'd better get patched up. Let's get out of here!",
                "We'd better get patched up and move on."
            };

            if (player.IsAlive)
            {
                yield return Dialogue.FirstMatchingMainCharacterSay(player, line1);
                yield return Dialogue.FirstMatchingMainCharacterSay(player, line2);
                yield return Dialogue.FirstMatchingMainCharacterSay(player, line3);
            }
            else
            {
                yield return Dialogue.FirstLiveMainCharacterSay(line1);
                yield return Dialogue.FirstLiveMainCharacterSay(line2);
                yield return Dialogue.FirstLiveMainCharacterSay(line3);
            }
        }

        private bool IsPlayerCharacter(Character character)
        {
            return character && character.Faction == Battle.PlayerFaction;
        }
    }
}