using System.Collections;
using UnityEngine;

namespace VerticalSlice
{
    [RequireComponent(typeof(Character))]
    public class BattleSequence_RandomGoblinFightStart : BattleSequence
    {
        private Character _goblin;

        protected override void Awake()
        {
            base.Awake();
            _goblin = GetComponent<Character>();
        }

        public override bool ShouldPlay(BattleEvent e)
        {
            return e.Type == BattleEvent.EventType.MoveStart && !HasPlayed && (e.Interactor == _goblin || e.Target == _goblin) && e.Interactor && e.Target;
        }

        protected override IEnumerator GetSequence(BattleEvent e)
        {
            Character other = e.Interactor == _goblin ? e.Target : e.Interactor;
            yield return _goblin.Say("Glory to Ha'all!");
            yield return Dialogue.FirstMatchingMainCharacterSay(other,
                "Ha'all? Who's Ha'all?",
                "Ha'all?",
                "What are you talking about?!",
                "Ha'all?");
            yield return _goblin.Say("You'll see him soon enough!! GRAAHA!!");
        }
    }
}